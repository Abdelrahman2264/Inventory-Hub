using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        public Task<Department> CreateDepartmentAsync(Department department);
        public Task<Department> EditDepartmentAsync(Department department);
        public Task<Department> GetDepartmentAsync(int id);
        public Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        public Task<bool> DeleteDepartmentAsync(int id);
        public Task<bool> CheckIfDepartmentExist(string name, int id = 0);


    }
}
