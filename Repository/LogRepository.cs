using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<LogRepository> logger;
        public LogRepository(InventoryDb _inventoryDb, ILogger<LogRepository> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }
        public async Task CreateLog(LogsHistory logsHistory)
        {
            try
            {
                await inventoryDb.LogsHistories.AddAsync(logsHistory);
                await inventoryDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Create New Log: {ex.Message}", ex);
            }

        }
        public async Task<IEnumerable<LogsHistory>> GetAllLogsAsync()
        {
            try
            {
                return await inventoryDb.LogsHistories.Include(u => u.AppUser).ToListAsync();
            }

            catch (Exception ex)
            {
                logger.LogError($"Error In Get All Logs: {ex.Message}", ex);
                return Enumerable.Empty<LogsHistory>();
            }
        }
    }
}
