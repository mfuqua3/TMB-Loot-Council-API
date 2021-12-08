using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LootCouncil.Domain;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.ThatsMyBis;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Wowhead;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LootCouncil.Engine
{
    public class ThatsMyBisDataEngine : IThatsMyBisDataEngine
    {
        private readonly ILogger<ThatsMyBisDataEngine> _logger;
        private readonly IWowheadClient _wowheadClient;
        private int _checkpoint;
        private const int CheckpointIncrement = 5;
        private readonly Dictionary<int, Item> _itemCache = new();
        private readonly LootCouncilDbContext _importProgressContext;
        private readonly LootCouncilDbContext _workerContext;
        private object _importProgressLock = new object();

        public ThatsMyBisDataEngine(
            IDbContextFactory<LootCouncilDbContext> dbContextFactory,
            ILogger<ThatsMyBisDataEngine> logger,
            IWowheadClient wowheadClient)
        {
            _importProgressContext = dbContextFactory.CreateDbContext();
            _workerContext = dbContextFactory.CreateDbContext();
            _logger = logger;
            _wowheadClient = wowheadClient;
        }

        public async Task ImportData(int importId, TmbRosterState tmbRosterState)
        {
            Import import;
            lock (_importProgressLock)
            {
                import =  _importProgressContext.Imports.Find(importId);
            }
            if (import == null)
            {
                var message = "An unexpected error occurred when importing TMB data. " +
                              "The import resource could not be located in the database. " +
                              $"The provided ID was {importId}";
                _logger.LogError(message);
                throw new KeyNotFoundException(message);
            }

            void UpdateProgress(object _, double progress)
            {
                import.Progress = progress;
                if (progress < _checkpoint)
                {
                    return;
                }

                if (progress >= 100)
                {
                    import.Completed = true;
                    import.Progress = 100;
                }
                _checkpoint += CheckpointIncrement;
                lock (_importProgressLock)
                {
                    _importProgressContext.SaveChanges();
                }
            }

            var importProgress = new Progress<double>();
            importProgress.ProgressChanged += UpdateProgress;
            try
            {
                await RunImport(tmbRosterState, import.GuildId, importProgress);
                UpdateProgress(this, 100);
                await _workerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                import.Faulted = true;
                import.Error = ex.Message;
                lock (_importProgressLock)
                { 
                    _importProgressContext.SaveChanges();
                }
                throw;
            }
            finally
            {
                importProgress.ProgressChanged -= UpdateProgress;
            }
        }

        private async Task RunImport(TmbRosterState tmbRosterState, int guildId, IProgress<double> progress)
        {
            double totalProgress = 0;
            await ClearGuildData(guildId);
            totalProgress += 5;
            progress.Report(totalProgress);
            var itemsToImport =
                tmbRosterState.Characters.Select(x => x.Prios.Count + x.Received.Count + x.Wishlist.Count).Sum();
            var perItemProgress = (100 - totalProgress) / itemsToImport;
            foreach (var tmbCharacter in tmbRosterState.Characters)
            {
                var totalItems = tmbCharacter.Prios.Count + tmbCharacter.Received.Count + tmbCharacter.Wishlist.Count;
                var character = await ImportCharacter(tmbCharacter, guildId);
                await ImportItems(tmbCharacter, character);
                totalProgress += perItemProgress * totalItems;
                progress.Report(totalProgress);
            }
        }

        private async Task ImportItems(TmbCharacter tmbCharacter, Character character)
        {
            character.CharacterItems = new List<CharacterItem>();
            foreach (var wishlistItem in tmbCharacter.Wishlist)
            {
                var item = await GetOrAddItem(wishlistItem);
                var characterItem = new CharacterItem
                {
                    Item = item,
                    Type = DataConstants.ItemTypes.Wishlist,
                    Date = wishlistItem.Details.UpdatedAt ?? wishlistItem.Details.CreatedAt,
                    Order = wishlistItem.Details.Order,
                };
                character.CharacterItems.Add(characterItem);
            }

            foreach (var wishlistItem in tmbCharacter.Received)
            {
                var item = await GetOrAddItem(wishlistItem);
                var characterItem = new CharacterItem
                {
                    Item = item,
                    Type = DataConstants.ItemTypes.Received,
                    Date = wishlistItem.Details.ReceivedAt
                };
                character.CharacterItems.Add(characterItem);
            }

            foreach (var wishlistItem in tmbCharacter.Prios)
            {
                var item = await GetOrAddItem(wishlistItem);
                var characterItem = new CharacterItem
                {
                    Item = item,
                    Type = DataConstants.ItemTypes.Priority
                };
                character.CharacterItems.Add(characterItem);
            }
        }

        private async Task<Item> GetOrAddItem(TmbItem tmbItem)
        {
            if (_itemCache.ContainsKey(tmbItem.ItemId))
            {
                return _itemCache[tmbItem.ItemId];
            }

            var item = await _workerContext
                .Items
                .AsQueryable()
                .SingleOrDefaultAsync(x => x.ItemId == tmbItem.ItemId);
            if (item != null)
            {
                _itemCache.Add(tmbItem.ItemId, item);
                return item;
            }

            var wowheadItem = await _wowheadClient.Get(tmbItem.ItemId.ToString());
            if (wowheadItem?.Item == null)
            {
                throw new ArgumentException($"Failed to fetch WowHead data for the item ID = {tmbItem.Id}");
            }

            item = new Item
            {
                ItemId = tmbItem.Id,
                Class = int.Parse(wowheadItem.Item.Class.Id),
                Subclass = int.Parse(wowheadItem.Item.Subclass.Id),
                Domain = "tbc",
                Icon = wowheadItem.Item.Icon.IconName,
                InventorySlot = int.Parse(wowheadItem.Item.InventorySlot.Id),
                Name = wowheadItem.Item.Name,
                ItemLevel = int.Parse(wowheadItem.Item.Level),
                Quality = wowheadItem.Item.Quality.Text,
                QualityValue = int.Parse(wowheadItem.Item.Quality.Id)
            };
            await _workerContext.AddAsync(item);
            _itemCache.Add(tmbItem.ItemId, item);
            return item;
        }

        private async Task<Character> ImportCharacter(TmbCharacter tmbCharacter, int guildId)
        {
            var character = new Character
            {
                GuildId = guildId,
                Class = tmbCharacter.Class,
                Race = tmbCharacter.Race,
                Name = tmbCharacter.Name
            };
            var user = await _workerContext.Users
                .AsQueryable()
                .FirstOrDefaultAsync(x =>
                    tmbCharacter.DiscordId.HasValue && x.DiscordIdentity.Id == tmbCharacter.DiscordId);
            if (user != null)
            {
                var guildUser = await _workerContext.GuildUsers
                    .AsQueryable()
                    .SingleOrDefaultAsync(x => x.GuildId == guildId && x.UserId == user.Id);
                guildUser ??= new GuildUser
                {
                    User = user
                };
                character.GuildUser = guildUser;
            }

            await _workerContext.Characters.AddAsync(character);
            return character;
        }

        private async Task ClearGuildData(int guildId)
        {
            var guildCharacters = _workerContext.Characters
                .AsQueryable()
                .Where(x => x.GuildId == guildId);
            _workerContext.RemoveRange(guildCharacters);
            await _workerContext.SaveChangesAsync();
        }
    }
}