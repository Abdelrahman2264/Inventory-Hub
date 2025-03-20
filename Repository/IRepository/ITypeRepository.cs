using Type = InventorySystem.ViewModels.Type;
using InventorySystem.Data;
namespace InventorySystem.Repository.IRepository
{
    public interface ITypeRepository
    {
        public Task<Type> Create(Type obj);
        public Task<Type> Edit(Type obj);
        public Task<Type> Get(int id);
        public Task<bool> Delete(int id);
        public Task<IEnumerable<Type>> GetAll();
        public Task<Type> getType(int id);
        public Task<bool> CheckIfTypeExist(string name , int id = 0);

    }
}
