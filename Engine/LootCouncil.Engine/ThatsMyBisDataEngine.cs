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
        private readonly LootCouncilDbContext _dbContext;
        private readonly ILogger<ThatsMyBisDataEngine> _logger;
        private readonly IWowheadClient _wowheadClient;
        private int _checkpoint;
        private const int CheckpointIncrement = 10;
        private readonly Dictionary<int, Item> _itemCache = new();

        public ThatsMyBisDataEngine(
            LootCouncilDbContext dbContext,
            ILogger<ThatsMyBisDataEngine> logger,
            IWowheadClient wowheadClient)
        {
            _dbContext = dbContext;
            _logger = logger;
            _wowheadClient = wowheadClient;
        }

        public async Task ImportData(int importId, TmbRosterState tmbRosterState)
        {
            var import = await _dbContext.Imports.FindAsync(importId);
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

                _checkpoint += CheckpointIncrement;
                _dbContext.Database.CommitTransaction();
                _dbContext.Database.BeginTransaction();
            }

            var importProgress = new Progress<double>();
            importProgress.ProgressChanged += UpdateProgress;
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                await RunImport(tmbRosterState, import.GuildId, importProgress);
                import.Progress = 100;
                import.Completed = true;
                await _dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                import.Faulted = true;
                import.Error = ex.Message;
                await _dbContext.SaveChangesAsync();
                throw;
            }
            finally
            {
                importProgress.ProgressChanged -= UpdateProgress;
            }
        }

        private async Task RunImport(TmbRosterState tmbRosterState, int guildId, IProgress<double> progress)
        {
            await ClearGuildData(guildId);
            const int guildDataProgress = 5;
            progress.Report(guildDataProgress);
            var itemsToImport =
                tmbRosterState.Characters.Select(x => x.Prios.Count + x.Received.Count + x.Wishlist.Count).Sum();
            var perItemProgress = ((double)100 - guildDataProgress) / itemsToImport;
            foreach (var tmbCharacter in tmbRosterState.Characters)
            {
                var totalItems = tmbCharacter.Prios.Count + tmbCharacter.Received.Count + tmbCharacter.Wishlist.Count;
                var character = await ImportCharacter(tmbCharacter, guildId);
                await ImportItems(tmbCharacter, character);
                progress.Report(perItemProgress * totalItems);
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

            var item = await _dbContext
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
            await _dbContext.AddAsync(item);
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
            var user = await _dbContext.Users
                .AsQueryable()
                .FirstOrDefaultAsync(x =>
                    tmbCharacter.DiscordId.HasValue && x.DiscordIdentity.Id == tmbCharacter.DiscordId);
            if (user != null)
            {
                var guildUser = await _dbContext.GuildUsers
                    .AsQueryable()
                    .SingleOrDefaultAsync(x => x.GuildId == guildId && x.UserId == user.Id);
                guildUser ??= new GuildUser
                {
                    User = user
                };
                character.GuildUser = guildUser;
            }

            await _dbContext.Characters.AddAsync(character);
            return character;
        }

        private async Task ClearGuildData(int guildId)
        {
            var guildCharacters = _dbContext.Characters
                .AsQueryable()
                .Include(x => x.CharacterItems)
                .ThenInclude(x => x.CharacterItemFilters)
                .Where(x => x.GuildId == guildId);
            _dbContext.RemoveRange(guildCharacters);
            await _dbContext.SaveChangesAsync();
        }
    }
}