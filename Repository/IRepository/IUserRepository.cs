using InventorySystem.Data;
using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User> EditUserAsync(User user);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<IEnumerable<User>> SearchUsersAsync(string name);
        public Task<User> GetUserAsync(int id);
        public Task<bool> DeleteUserAsync(int id);

        public Task<bool> isPhoneExist(string phone, int id);

        public Task<bool> isEmailExist(string email, int id);

        public Task<bool> isFingerPrintExist(string fingerprint, int id);


    }
}
