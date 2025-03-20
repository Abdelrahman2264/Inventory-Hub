using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Spreadsheet;
using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<AssignmentRepository> logger;
        public AssignmentRepository(InventoryDb _inventoryDb, ILogger<AssignmentRepository> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }
        public async Task<Assignment> AssignToAsync(Assignment assignment)
        {
            try
            {
                assignment.AssignedDate = DateTime.Now;
                assignment.IsReturned = false;

                var asset = await inventoryDb.Assets.FirstOrDefaultAsync(a => a.AssetId == assignment.AssetId);
                var user = await inventoryDb.Users.FirstOrDefaultAsync(u => u.UserId == assignment.UserId);
                if (asset == null)
                {
                    throw new Exception("Asset not found.");
                }

                asset.Status = "Assigned";


                if (asset.Type.Name.ToLower() == "DeskTop".ToLower())
                {
                    var desk = await inventoryDb.Desktops.FirstOrDefaultAsync(u => u.AssetId == assignment.AssetId);
                    var deskuser = await inventoryDb.Users.FirstOrDefaultAsync(u => u.UserId == assignment.UserId);
                    var usersite = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.SiteId == deskuser.SiteId);
                    desk.DeviceName = usersite.SiteCode + "-" + desk.Label + "-" + deskuser.FirstName;
                    inventoryDb.Update(desk);
                }
                if (asset.Type.Name.ToLower() == "LapTop".ToLower())
                {
                    var lap = await inventoryDb.Laptops.FirstOrDefaultAsync(u => u.AssetId == assignment.AssetId);
                    var lapuser = await inventoryDb.Users.FirstOrDefaultAsync(u => u.UserId == assignment.UserId);
                    var usersite = await inventoryDb.Sites.FirstOrDefaultAsync(u => u.SiteId == lapuser.SiteId);
                    lap.DeviceName = usersite.SiteCode + "-" + lap.Label + "-" + lapuser.FirstName;
                    inventoryDb.Update(lap);
                }

                inventoryDb.Update(asset);
                await inventoryDb.Assignments.AddAsync(assignment);
                await inventoryDb.SaveChangesAsync();
                return assignment;
            }

            catch (Exception ex)
            {
                logger.LogError($"Error In Assigning Asset: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<IEnumerable<Assignment>> GetAllAssignments()
        {
            try
            {
                return await inventoryDb.Assignments
                    .Include(a => a.Asset)
                    .ThenInclude(a => a.Type)
                    .Include(a => a.User)
                    .ThenInclude(u => u.Department)
                    .Include(d => d.User)
                    .ThenInclude(u => u.Site)
                    .Where(a => a.ReturnedDate == null && a.IsReturned == false)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Getting All Assignments: {ex.Message}", ex);
                return Enumerable.Empty<Assignment>();
            }

        }
        public async Task<IEnumerable<Assignment>> GetAllAssignmentsReturned()
        {
            try
            {
                return await inventoryDb.Assignments
                    .Include(a => a.Asset)
                    .ThenInclude(a => a.Type)
                    .Include(a => a.User)
                    .ThenInclude(u => u.Department)
                    .Include(d => d.User)
                    .ThenInclude(u => u.Site)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Getting All Assignments: {ex.Message}", ex);
                return Enumerable.Empty<Assignment>();

            }
        }

        public async Task<IEnumerable<Assignment>> GetAssignmetsByIdAsync(int id)
        {
            try
            {
                return await inventoryDb.Assignments.Where(u => u.UserId == id)
                          .Include(a => a.Asset)
                          .ThenInclude(a => a.Type)
                          .Include(a => a.User)
                          .ThenInclude(u => u.Department)
                          .Include(d => d.User)
                          .ThenInclude(u => u.Site)
                          .Where(a => a.ReturnedDate == null && a.IsReturned == false)
                          .ToListAsync();
            }

            catch (Exception ex)
            {
                logger.LogError($"Error In Getting All Assignments By Id: {ex.Message}", ex);
                return Enumerable.Empty<Assignment>();
            }
        }

        public Assignment GetByIdAsync(int id)
        {
            try
            {

                var assignment = inventoryDb.Assignments.FirstOrDefault(u => u.AssetId == id && u.IsReturned == false);
                if (assignment != null)
                {
                    return assignment;
                }
                return new Assignment();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Getting Assignment By Id: {ex.Message}", ex);
                return new Assignment();

            }
        }
        public async Task<bool> ReturnFromAsync(int id)
        {
            try
            {
                var assignment = await inventoryDb.Assignments
                    .Include(a => a.Asset).ThenInclude(u => u.Type) // Ensure Asset is included
                    .FirstOrDefaultAsync(u => u.AssignmentId == id);

                if (assignment == null)
                {
                    return false;
                }

                if (assignment.Asset != null)
                {
                    assignment.Asset.Status = "Stock";
                    assignment.ReturnedDate = DateTime.Now;
                    assignment.Notes = "The Device Reclaim From The User";
                    assignment.IsReturned = true;

                    if (assignment.Asset.Type.Name.ToLower() == "LapTop".ToLower())
                    {
                        var laptop = await inventoryDb.Laptops.FirstOrDefaultAsync(u => u.AssetId == assignment.AssetId);
                        if (laptop != null)
                        {
                            laptop.DeviceName = "Stock LapTop";
                            inventoryDb.Update(laptop);
                        }
                    }
                    if (assignment.Asset.Type.Name.ToLower() == "DeskTop".ToLower())
                    {
                        var desktop = await inventoryDb.Desktops.FirstOrDefaultAsync(u => u.AssetId == assignment.AssetId);
                        if (desktop != null)
                        {
                            desktop.DeviceName = "Stock DeskTop";
                            inventoryDb.Update(desktop);

                        }


                    }
                    inventoryDb.Update(assignment.Asset);
                }


                return (await inventoryDb.SaveChangesAsync() > 0);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Reclaim an Asset : {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> isUsed(int id)
        {
            try
            {
                var assets = await inventoryDb.Assignments.FirstOrDefaultAsync(u => u.IsReturned == true && u.AssetId == id);
                if (assets != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Check If Asset Is Returned an Asset : {ex.Message}", ex);
                return false;
            }
        }

    }
}
