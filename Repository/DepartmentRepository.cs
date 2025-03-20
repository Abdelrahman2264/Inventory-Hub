using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<DepartmentRepository> logger;
        public DepartmentRepository(InventoryDb _inventoryDb, ILogger<DepartmentRepository> logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = logger;
        }
        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            try
            {
                department.IsActive = true;
                await inventoryDb.Departments.AddAsync(department);
                await inventoryDb.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Create New Department: {ex.Message}", ex);
                return new Department();
            }

        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            try
            {
                var department = await inventoryDb.Departments.FirstOrDefaultAsync(u => u.DepartmentId == id);
                if (department != null)
                {
                    department.IsActive = false;
                    inventoryDb.Departments.Update(department);
                    return (await inventoryDb.SaveChangesAsync() > 0);
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In InActive  Department: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<Department> EditDepartmentAsync(Department department)
        {
            try
            {
                var UpdatedDepartment = await inventoryDb.Departments.FirstOrDefaultAsync(u => u.DepartmentId == department.DepartmentId);
                if (UpdatedDepartment != null)
                {
                    UpdatedDepartment.Name = department.Name;
                    department.IsActive = true;
                    inventoryDb.Update(UpdatedDepartment);
                    await inventoryDb.SaveChangesAsync();
                    return UpdatedDepartment;
                }
                return department;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Update Department: {ex.Message}", ex);
                return new Department();
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            try
            {


                return await inventoryDb.Departments.OrderBy(u => u.DepartmentId).Where(u => u.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get All Departments: {ex.Message}", ex);
                return Enumerable.Empty<Department>();
            }
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            try
            {
                var department = await inventoryDb.Departments.FirstOrDefaultAsync(u => u.DepartmentId == id && u.IsActive == true);
                if (department != null)
                {
                    return department;

                }
                return new Department();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error In Get Department: {ex.Message}", ex);
                return new Department();
            }
            }
        public async Task<bool> CheckIfDepartmentExist(string name, int id = 0)
        {
            try
            {
                if (id != 0)
                {
                    var department = await inventoryDb.Departments.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.IsActive == true && u.DepartmentId != id);
                    if (department != null)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    var department = await inventoryDb.Departments.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.IsActive == true);
                    if (department != null)
                    {
                        return true;
                    }
                    return false;
                }


            }catch (Exception ex)
            {
                logger.LogError($"Error In Check If Department Exist: { ex.Message}", ex);
                return false;
            }
        }
    }
}
 