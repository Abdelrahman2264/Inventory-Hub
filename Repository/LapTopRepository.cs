using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repository
{
    public class LapTopRepository : ILapTopRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<LapTopRepository> logger;
        public LapTopRepository(InventoryDb _inventoryDb, ILogger<LapTopRepository> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }
        public async Task<Laptop> CreateLapTopAsync(Laptop laptop)
        {
            try
            {

                var label = await GenerateNextLabelAsync();
                laptop.DeviceName = "New LapTop";
                laptop.Label = label;
                await inventoryDb.Laptops.AddAsync(laptop);
                await inventoryDb.SaveChangesAsync();
                return laptop;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Create New Laptop: {ex.Message}", ex);
                return new Laptop();
            }
        }

        public async Task<Laptop> EditLapTopAsync(Laptop laptop)
        {
            try
            {

                var UpdatedLaptop = await inventoryDb.Laptops.FirstOrDefaultAsync(u => u.AssetId == laptop.AssetId);
                if (UpdatedLaptop != null)
                {
                    UpdatedLaptop.Ram = laptop.Ram;
                    UpdatedLaptop.Cpu = laptop.Cpu;
                    UpdatedLaptop.Gpu = laptop.Gpu;
                    UpdatedLaptop.MacWifi = laptop.MacWifi;
                    UpdatedLaptop.MacEthernet = laptop.MacEthernet;
                    UpdatedLaptop.HardDisk = laptop.HardDisk;
                    UpdatedLaptop.ScreenSize = laptop.ScreenSize;
                    UpdatedLaptop.Label = laptop.Label;
                    UpdatedLaptop.OS = laptop.OS;
                    await inventoryDb.SaveChangesAsync();
                    return UpdatedLaptop;
                }
                return laptop;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Edit LapTop {ex.Message}", ex);
                return new Laptop();
            }

        }

        public async Task<IEnumerable<Laptop>> GetAllLapTopsAsync()
        {
            try
            {
                return await inventoryDb.Laptops
                    .OrderBy(u => u.AssetId)
                    .Include(u => u.Asset)
                    .Include(t => t.Asset)
                    .ThenInclude(t => t.Type)
                    .Where(t => t.Asset.Status.ToLower() != "EndLife".ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Get All LapTops {ex.Message}", ex);
                return new List<Laptop>();
            }


        }

        public async Task<Laptop> GetLapTopAsync(int id)
        {
            try
            {
                var laptop = await inventoryDb.Laptops.FirstOrDefaultAsync(u => u.AssetId == id);
                if (laptop != null)
                {
                    return laptop;

                }
                return new Laptop();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Get LapTop {ex.Message}", ex);
                return new Laptop();
            }
        }
   
        
        public async Task<string> GenerateNextLabelAsync()
        {
            // Retrieve the latest label from the database
            var latestLabel = await inventoryDb.Laptops
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
