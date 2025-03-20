using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repository
{
    public class DeskTopRepsoitory : IDeskTopRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<DeskTopRepsoitory> logger;
        public DeskTopRepsoitory(InventoryDb _inventoryDb, ILogger<DeskTopRepsoitory> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }
        public async Task<Desktop> CreateDeskTopAsync(Desktop Desktop)
        {
            try
            {
                var Label = await GenerateNextLabelAsync();
                Desktop.DeviceName = "New DeskTop";
                Desktop.Label = Label;
                await inventoryDb.Desktops.AddAsync(Desktop);
                await inventoryDb.SaveChangesAsync();
                return Desktop;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Create New Desktop: {ex.Message}", ex);
                return new Desktop();
            }
        }

        public async Task<Desktop> EditDeskTopAsync(Desktop Desktop)
        {
            try
            {
                var UpdatedDesktop = await inventoryDb.Desktops.FirstOrDefaultAsync(u => u.AssetId == Desktop.AssetId);
                if (UpdatedDesktop != null)
                {
                    UpdatedDesktop.Ram = Desktop.Ram;
                    UpdatedDesktop.Cpu = Desktop.Cpu;
                    UpdatedDesktop.Gpu = Desktop.Gpu;
                    UpdatedDesktop.MacWifi = Desktop.MacWifi;
                    UpdatedDesktop.MacEthernet = Desktop.MacEthernet;
                    UpdatedDesktop.HardDisk = Desktop.HardDisk;
                    UpdatedDesktop.Label = Desktop.Label;
                    UpdatedDesktop.OS = Desktop.OS;
                    await inventoryDb.SaveChangesAsync();
                    return UpdatedDesktop;
                }
                return Desktop;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Edit Desktop: {ex.Message}", ex);
                return new Desktop();
            }

        }

        public async Task<IEnumerable<Desktop>> GetAllDeskTopsAsync()
        {
            try
            {
                return await inventoryDb.Desktops.Where(u => u.Asset.Status.ToLower() != "EndLife".ToLower())
                    .OrderBy(u => u.AssetId)
                    .Include(u => u.Asset)
                    .ThenInclude(u => u.Type)
                    .Include(u => u.Asset)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get All Desktops: {ex.Message}", ex);
                return Enumerable.Empty<Desktop>();
            }


        }

        public async Task<Desktop> GetDeskTopAsync(int id)
        {
            try
            {
                var Desktop = await inventoryDb.Desktops.FirstOrDefaultAsync(u => u.AssetId == id);
                if (Desktop != null)
                {
                    return Desktop;

                }
                return new Desktop();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get Desktop: {ex.Message}", ex);
                return new Desktop();
            }
        }
       
       
        public async Task<string> GenerateNextLabelAsync()
        {
            // Retrieve the latest label from the database
            var latestLabel = await inventoryDb.Desktops
                .OrderByDescending(c => c.Label)
                .Select(c => c.Label)
                .FirstOrDefaultAsync();

            // If no labels exist, start from L001
            if (string.IsNullOrEmpty(latestLabel))
                return "L001";

            // Extract numeric part from the latest label (e.g., L001 -> 001)
            int latestNumber = int.Parse(latestLabel.Substring(1));

            // Increment the number
            int nextNumber = latestNumber + 1;

            // Format the new label (L001, L002, ..., L999)
            return $"L{nextNumber:D3}";
        }
    }
}
