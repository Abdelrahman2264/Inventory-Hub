using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface ISiteRepository
    {
        Task<IEnumerable<Site>> GetAllSitesAsync();
        Task<Site> GetSiteAsync(int id);
        Task<Site> CreateSiteAsync(Site site);
        Task<Site> EditSiteAsync(Site site);
        Task<bool> DeleteSiteASync(int id);
        public Task<bool> CheckIfSiteExist(string name, int id = 0);
        public Task<bool> CheckIfSiteCodeExist(string name, int id = 0);

    }
}
