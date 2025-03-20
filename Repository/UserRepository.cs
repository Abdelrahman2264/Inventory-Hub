using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;

namespace InventorySystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<UserRepository> logger;
        public UserRepository(InventoryDb _inventoryDb, ILogger<UserRepository> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                await inventoryDb.Users.AddAsync(user);
                await inventoryDb.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Add New User : {ex.Message}", ex);
                return new User();
            }

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await inventoryDb.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (user != null)
                {
                    var assetsassgined = await inventoryDb.Assignments.Where(u => u.UserId == user.UserId && u.IsReturned == false).Include(u => u.Asset).ThenInclude(u => u.Type).ToListAsync();

                    foreach (var asset in assetsassgined)
                    {
                        asset.Asset.Status = "Stock";
                        asset.ReturnedDate = DateTime.Now;
                        asset.Notes = "The Device Reclaim From The User";
                        asset.IsReturned = true;
                        if (asset.Asset.Type.Name.ToLower() == "LapTop".ToLower())
                        {
                            var laptop = await inventoryDb.Laptops.FirstOrDefaultAsync(u => u.AssetId == asset.AssetId);
                            if (laptop != null)
                            {
                                laptop.DeviceName = "Stock LapTop";
                                inventoryDb.Update(laptop);
                            }
                        }
                        if (asset.Asset.Type.Name.ToLower() == "DeskTop".ToLower())
                        {
                            var desktop = await inventoryDb.Desktops.FirstOrDefaultAsync(u => u.AssetId == asset.AssetId);
                            if (desktop != null)
                            {
                                desktop.DeviceName = "Stock DeskTop";

                                inventoryDb.Update(desktop);

                            }
                        }
                    }
                    user.IsActive = false;
                    inventoryDb.Update(user);
                    return (await inventoryDb.SaveChangesAsync() > 0);
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In InActive User : {ex.Message}", ex);
                return false;
            }
        }


        public async Task<User> EditUserAsync(User user)
        {
            try
            {

                var UpdatedUser = await inventoryDb.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);

                if (UpdatedUser != null)
                {
                    var assetsassgined = await inventoryDb.Assignments.Where(u => u.UserId == user.UserId && u.IsReturned == false).Include(u => u.Asset).ThenInclude(u => u.Type).ToListAsync();
                    foreach (var asset in assetsassgined)
                    {
                        if (asset.Asset.Type.Name.ToLower() == "LapTop".ToLower())
                        {
                            var laptop = await inventoryDb.Laptops.FirstOrDefaultAsync(u => u.AssetId == asset.AssetId);
                            if (laptop != null)
                            {
                                laptop.DeviceName = user.Site.SiteCode + "-" + laptop.Label + "-" + user.FirstName;
                                inventoryDb.Update(laptop);
                            }
                        }
                        if (asset.Asset.Type.Name.ToLower() == "DeskTop".ToLower())
                        {
                            var desktop = await inventoryDb.Desktops.FirstOrDefaultAsync(u => u.AssetId == asset.AssetId);
                            if (desktop != null)
                            {
                                desktop.DeviceName = user.Site.SiteCode + "-" + desktop.Label + "-" + user.FirstName;
                                inventoryDb.Update(desktop);

                            }
                        }

                    }
                    UpdatedUser.FirstName = user.FirstName;
                    UpdatedUser.LastName = user.LastName;
                    UpdatedUser.SiteId = user.SiteId;
                    UpdatedUser.Email = user.Email;
                    UpdatedUser.Phone = user.Phone;
                    UpdatedUser.DepartmentId = user.DepartmentId;
                    UpdatedUser.Fingerprint = user.Fingerprint;
                    UpdatedUser.JobTitle = user.JobTitle;
                    UpdatedUser.Role = "User";
                    UpdatedUser.ManagerId = user.ManagerId;
                    UpdatedUser.IsActive = true;
                    inventoryDb.Update(UpdatedUser);
                    await inventoryDb.SaveChangesAsync();
                    return UpdatedUser;
                }
                return user;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Edit User : {ex.Message}", ex);
                return new User();
            }

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {

                return await inventoryDb.Users.OrderBy(u => u.FirstName)
                    .Include(s => s.Site)
                    .Include(m => m.Manager)
                    .Include(d => d.Department)
                    .Where(a => a.IsActive)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get All Users : {ex.Message}", ex);
                return Enumerable.Empty<User>();
            }

        }

        public async Task<User> GetUserAsync(int id)
        {
            try
            {
                var user = await inventoryDb.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (user != null)
                {
                    return user;
                }
                return new User();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get User : {ex.Message}", ex);
                return new User();
            }
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string name)
        {
            try
            {
                var users = await inventoryDb.Users
                   .Where(d => EF.Functions.Like(d.FirstName + " " + d.LastName, $"%{name}%") || EF.Functions.Like(d.Fingerprint, $"%{name}%")).Take(5)
                   .Where(a => a.IsActive)
                   .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Serach Users : {ex.Message}", ex);
                return Enumerable.Empty<User>();

            }


        }
        public async Task<bool> isPhoneExist(string phone, int id)
        {
            try
            {
                var flag = await inventoryDb.Users.FirstOrDefaultAsync(u => u.Phone.ToLower() == phone.ToLower() && u.IsActive == true && u.UserId != id);
                if (flag != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Check Phone Exist : {ex.Message}", ex);
                return false;
            }
        }
        public async Task<bool> isEmailExist(string email, int id)
        {
            try
            {
                var flag = await inventoryDb.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.IsActive == true && u.UserId != id);
                if (flag != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Check Email Exist : {ex.Message}", ex);
                return false;
            }
        }
        public async Task<bool> isFingerPrintExist(string fingerprint, int id)
        {
            try
            {
                var flag = await inventoryDb.Users.FirstOrDefaultAsync(u => u.Fingerprint.ToLower() == fingerprint.ToLower() && u.IsActive == true && u.UserId != id);
                if (flag != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Check Fingerprint Exist : {ex.Message}", ex);
                return false;
            }
        }
    }
}
