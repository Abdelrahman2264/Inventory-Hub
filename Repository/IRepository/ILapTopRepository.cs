using InventorySystem.Data;
using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface ILapTopRepository
    {
        public Task<Laptop> CreateLapTopAsync(Laptop laptop);
        public Task<Laptop> EditLapTopAsync(Laptop laptop);
        public Task<Laptop> GetLapTopAsync(int id);
        public Task<IEnumerable<Laptop>> GetAllLapTopsAsync();

      

    }
}
