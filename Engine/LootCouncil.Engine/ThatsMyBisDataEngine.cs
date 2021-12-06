using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.ThatsMyBis;
using Microsoft.Extensions.Logging;

namespace LootCouncil.Engine
{
    public class ThatsMyBisDataEngine : IThatsMyBisDataEngine
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly ILogger<ThatsMyBisDataEngine> _logger;

        public ThatsMyBisDataEngine(LootCouncilDbContext dbContext, ILogger<ThatsMyBisDataEngine> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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
                _dbContext.SaveChangesAsync();
            }
            var importProgress = new Progress<double>();
            importProgress.ProgressChanged += UpdateProgress;
            try
            {
                await RunImport(tmbRosterState, importProgress);
                import.Progress = 1;
                import.Completed = true;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
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

        private Task RunImport(TmbRosterState tmbRosterState, IProgress<double> progress)
        {
            throw new System.NotImplementedException();
        }
    }
}