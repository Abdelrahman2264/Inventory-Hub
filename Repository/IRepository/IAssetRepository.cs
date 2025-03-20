using InventorySystem.Data;
using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface IAssetRepository
    {
        public Task<Asset> CreateAssetAsync(Asset asset);
        public Task<Asset> EditAssetAsync(Asset asset);
        public Task<Asset> GetAssetAsync(int id);
        public Task<Asset> GetAssetAsyncSerial(string serial);
        public Task<IEnumerable<Asset>> GetAllAssetAsync();
        public Task<IEnumerable<Asset>> GetAllAssetInTableAsync();
        public Task<bool> DeleteAssetAsync(int id);
        public Task<IEnumerable<Asset>> SearchAssetAsync(string serial);
        public Task<bool> isSerialNumberExist(string serial , int id);



    }
}
