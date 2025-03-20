using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface IMaintenanceRepository
    {
        public Task<IEnumerable<Maintenance>> GetAllMaintenances();
        public Task<Maintenance> GetMaintenance(int maintenanceId);
        public Task<Maintenance> MaintainAsset(Maintenance maintenance);
        public Task<bool> DeleteMaintenance(Maintenance maintenance,int maintenanceId);
        public Task<IEnumerable<Maintenance>> GetAllMaintenancesHistory();

    }
}
