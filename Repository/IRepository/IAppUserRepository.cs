using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface IAppUserRepository
    {
        public Task<AppUsers> AddNewAppUserAsync(AppUsers appUsers);
        public Task<AppUsers> GetAppUserAsync(int id);
        public Task<IEnumerable<AppUsers>> GetAllAppUsersAsync();
        public Task<bool> InActiveAppUser(int id);

        public Task<AppUsers> EditAppUserAsync(AppUsers appUsers);

    }
}
