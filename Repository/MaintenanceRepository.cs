using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repository
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<MaintenanceRepository> logger;

        public MaintenanceRepository(InventoryDb _inventoryDb, ILogger<MaintenanceRepository> logger)
        {

            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }
        public async Task<bool> DeleteMaintenance(Maintenance maintenance, int maintenanceId)
        {
            try
            {

                var asset = await inventoryDb.Maintenances.FirstOrDefaultAsync(a => a.MaintenanceId == maintenanceId);

                if (asset != null)
                {
                    asset.Solution = maintenance.Solution;
                    asset.DateReturned = maintenance.DateReturned;
                    asset.UpdatedAt = DateTime.Now;
                    asset.Status = "Returned";
                    inventoryDb.Maintenances.Update(asset);
                    await inventoryDb.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Delete Maintenance {ex.Message}", ex);
                return false;
            }
        }

        public async Task<IEnumerable<Maintenance>> GetAllMaintenances()
        {
            try
            {
                return await inventoryDb.Maintenances
                    .Include(a => a.Asset)
                    .ThenInclude(t => t.Type)
                    .Where(a => a.Status.ToLower() == "In maintainence".ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get All Maintenances: {ex.Message}", ex);
                return Enumerable.Empty<Maintenance>();
            }

        }
        public async Task<IEnumerable<Maintenance>> GetAllMaintenancesHistory()
        {
            try
            {
                return await inventoryDb.Maintenances
                    .Include(a => a.Asset)
                    .ThenInclude(t => t.Type)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get All Maintenances History: {ex.Message}", ex);
                return Enumerable.Empty<Maintenance>();
            }
        }

        public async Task<Maintenance> GetMaintenance(int maintenanceId)
        {
            try
            {

                var asset = await inventoryDb.Maintenances.FirstOrDefaultAsync(a => a.MaintenanceId == maintenanceId);
                if (asset != null)
                {
                    return asset;
                }
                return new Maintenance();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Get Maintenance {ex.Message}", ex);
                return new Maintenance();
            }
        }

        public async Task<Maintenance> MaintainAsset(Maintenance maintenance)
        {
            try
            {
                maintenance.Status = "In maintainence";
                maintenance.CreatedAt = DateTime.UtcNow;
                await inventoryDb.Maintenances.AddAsync(maintenance);
                await inventoryDb.SaveChangesAsync();
                return maintenance;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Maintain Asset {ex.Message}", ex);
                return new Maintenance();
            }
        }

    }
}
