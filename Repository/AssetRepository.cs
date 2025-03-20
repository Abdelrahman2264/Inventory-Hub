using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventorySystem.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<Asset> logger;
        public AssetRepository(InventoryDb _inventoryDb, ILogger<Asset> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }
        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            try
            {
                asset.Status = "Stock";
                asset.CreatedDate = DateTime.Now;
                await inventoryDb.Assets.AddAsync(asset);
                await inventoryDb.SaveChangesAsync();
                return asset;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Adding New Asset: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            try
            {
                var asset = await inventoryDb.Assets.FirstOrDefaultAsync(u => u.AssetId == id);
                if (asset != null)
                {
                    asset.Status = "EndLife";
                    asset.UpdatedDate = DateTime.Now;
                    inventoryDb.Update(asset);
                    await inventoryDb.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Inactive Asset: {ex.Message}", ex);
                throw;

            }
        }

        public async Task<Asset> EditAssetAsync(Asset asset)
        {
            try
            {


                var UpdatedAsset = await inventoryDb.Assets.FirstOrDefaultAsync(u => u.AssetId == asset.AssetId);
                if (UpdatedAsset != null)
                {
                    UpdatedAsset.ModelVersion = asset.ModelVersion;
                    UpdatedAsset.PurchaseDate = asset.PurchaseDate;
                    UpdatedAsset.ExpiryDate = asset.ExpiryDate;
                    UpdatedAsset.Brand = asset.Brand;
                    UpdatedAsset.Description = asset.Description;
                    UpdatedAsset.SerialNumber = asset.SerialNumber;
                    UpdatedAsset.TypeId = asset.TypeId;
                    UpdatedAsset.UpdatedDate = DateTime.Now;
                    inventoryDb.Update(UpdatedAsset);
                    await inventoryDb.SaveChangesAsync();

                    return UpdatedAsset;
                }
                return asset;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Updating Asset: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Asset>> GetAllAssetAsync()
        {
            try
            {

                return await inventoryDb.Assets.
                    Where(u => u.Status.ToLower() != "EndLife".ToLower())
                    .OrderBy(u => u.AssetId)
                    .Include(u => u.Desktop)
                    .Include(u => u.Laptop)
                    .Include(u => u.Type)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Getting All Assets: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<IEnumerable<Asset>> GetAllAssetInTableAsync()
        {

            try
            {
                return await inventoryDb.Assets
                    .OrderBy(u => u.AssetId)
                    .Include(u => u.Desktop)
                    .Include(u => u.Laptop)
                    .Include(u => u.Type)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Getting All In The Table Assets: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<Asset> GetAssetAsync(int id)
        {
            try
            {


                var asset = await inventoryDb.Assets.FirstOrDefaultAsync(u => u.AssetId == id);
                if (asset != null)
                {
                    return asset;
                }
                return new Asset();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get Asset In Assets: {ex.Message}", ex);
                throw;
            }


        }
        public async Task<Asset> GetAssetAsyncSerial(string serial)
        {
            try
            {

                var asset = await inventoryDb.Assets.FirstOrDefaultAsync(u => u.SerialNumber.ToLower() == serial.ToLower());
                if (asset != null)
                {
                    return asset;
                }
                return new Asset();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get Asset In Assets By Serial: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<IEnumerable<Asset>> SearchAssetAsync(string serial)
        {
            try
            {
                var assets = await inventoryDb.Assets
                    .Where(d => EF.Functions.Like(d.SerialNumber, $"%{serial}%"))
                    .Take(5)
                    .Include(u => u.Type)
                    .ToListAsync();

                return assets;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Searching Asset: {ex.Message}", ex);
                throw;
            }

        }
        public async Task<bool> isSerialNumberExist(string serial, int id)
        {
            try
            {
                var isExist = await inventoryDb.Assets.FirstOrDefaultAsync(u => u.SerialNumber.ToLower() == serial.ToLower() && u.AssetId != id);
                if (isExist != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Checking Serial Number Exist: {ex.Message}", ex);
                throw;
            }
        }

    }
}
