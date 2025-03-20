using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface ILogRepository
    {
        public Task CreateLog(LogsHistory logsHistory);
        public Task<IEnumerable<LogsHistory>> GetAllLogsAsync();
        

    }
}
