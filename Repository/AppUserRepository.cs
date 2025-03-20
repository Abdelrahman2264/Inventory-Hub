using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<AppUserRepository> logger;

        public AppUserRepository(InventoryDb _inventoryDb, ILogger<AppUserRepository> _logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = _logger;
        }

        public async Task<AppUsers> AddNewAppUserAsync(AppUsers appUsers)
        {
            try
            {
                appUsers.IsActive = true;
                await inventoryDb.AppUsers.AddAsync(appUsers);
                await inventoryDb.SaveChangesAsync();
                return appUsers;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error adding new user: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<AppUsers> EditAppUserAsync(AppUsers appUsers)
        {
            try
            {
                var user = await inventoryDb.AppUsers.FirstOrDefaultAsync(u => u.AppUserId == appUsers.AppUserId && u.IsActive);
                if (user != null)
                {
                    user.LoginName = appUsers.LoginName;
                    user.Password = appUsers.Password;
                    user.Role = appUsers.Role;
                    user.Email = appUsers.Email;
                    inventoryDb.Update(user);
                    await inventoryDb.SaveChangesAsync();
                    return user;
                }
                return appUsers;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error editing user: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<AppUsers>> GetAllAppUsersAsync()
        {
            try
            {
                return await inventoryDb.AppUsers.Where(u => u.IsActive).OrderBy(u => u.AppUserId).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error retrieving all users: {ex.Message}", ex);
                return Enumerable.Empty<AppUsers>();
            }
        }

        public async Task<AppUsers> GetAppUserAsync(int id)
        {
            try
            {
                var appUser = await inventoryDb.AppUsers.FirstOrDefaultAsync(u => u.AppUserId == id);
                return appUser ?? new AppUsers();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error retrieving user with ID {id}: {ex.Message}", ex);
                return new AppUsers();
            }
        }

        public async Task<bool> InActiveAppUser(int id)
        {
            try
            {
                var user = await inventoryDb.AppUsers.FirstOrDefaultAsync(u => u.AppUserId == id && u.IsActive);
                if (user != null)
                {
                    user.IsActive = false;
                    inventoryDb.Update(user);
                    return await inventoryDb.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error deactivating user with ID {id}: {ex.Message}", ex);
                return false;
            }
        }
    }
}
