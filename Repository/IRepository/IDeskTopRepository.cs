using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface IDeskTopRepository
    {
        public Task<Desktop> CreateDeskTopAsync(Desktop Desktop);
        public Task<Desktop> EditDeskTopAsync(Desktop Desktop);
        public Task<Desktop> GetDeskTopAsync(int id);
        public Task<IEnumerable<Desktop>> GetAllDeskTopsAsync();

    }
}
