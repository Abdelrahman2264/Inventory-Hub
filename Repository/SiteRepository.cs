using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repository
{
    public class SiteRepository : ISiteRepository
    {

        private readonly InventoryDb inventoryDb;
        private readonly ILogger<SiteRepository> logger;

        public SiteRepository(InventoryDb _inventoryDb, ILogger<SiteRepository> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }

        public async Task<Site> CreateSiteAsync(Site site)
        {
            try
            {
                site.isActive = true;
                await inventoryDb.Sites.AddAsync(site);
                await inventoryDb.SaveChangesAsync();
                return site;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Create a New Site: {ex.Message}", ex);
                return new Site();
            }

        }

        public async Task<bool> DeleteSiteASync(int id)
        {
            try
            {
                var site = inventoryDb.Sites.FirstOrDefault(u => u.SiteId == id);
                if (site != null)
                {
                    site.isActive = false;
                    inventoryDb.Sites.Update(site);
                    return (await inventoryDb.SaveChangesAsync() > 0);
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In InActive Site: {ex.Message}", ex);

                return false;

            }
        }

        public async Task<Site> EditSiteAsync(Site site)
        {
            try
            {
                var UpdatedSite = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.SiteId == site.SiteId);
                if (UpdatedSite != null)
                {
                    UpdatedSite.Name = site.Name;
                    site.isActive = true;
                    UpdatedSite.SiteCode = site.SiteCode;
                    inventoryDb.Sites.Update(UpdatedSite);
                    await inventoryDb.SaveChangesAsync();
                    return UpdatedSite;
                }
                return site;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Edit Site: {ex.Message}", ex);
                return new Site();
            }
        }

        public async Task<IEnumerable<Site>> GetAllSitesAsync()
        {
            try
            {
                return await inventoryDb.Sites.OrderBy(u => u.SiteId).Where(a => a.isActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get All Sites: {ex.Message}", ex);
                return Enumerable.Empty<Site>();
            }
        }

        public async Task<Site> GetSiteAsync(int id)
        {
            try
            {
                var site = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.SiteId == id && u.isActive == true);
                if (site != null)
                {
                    return site;
                }
                return new Site();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get Site: {ex.Message}", ex);
                return new Site();
            }
        }


        public async Task<bool> CheckIfSiteExist(string name, int id = 0)
        {
            try
            {


                if (id != 0)
                {
                    var site = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.isActive == true && u.SiteId != id);
                    if (site != null)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    var site = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.isActive == true);
                    if (site != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Check If Site Exist: {ex.Message}", ex);

                return false;
            }
        }
        public async Task<bool> CheckIfSiteCodeExist(string code, int id = 0)
        {
            try
            {


                if (id != 0)
                {
                    var site = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.SiteCode.ToLower() == code.ToLower() && u.isActive == true && u.SiteId != id);
                    if (site != null)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    var site = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.SiteCode.ToLower() == code.ToLower() && u.isActive == true);
                    if (site != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Check If Site Code Exist: {ex.Message}", ex);

                return false;
            }
        }
    }
}
