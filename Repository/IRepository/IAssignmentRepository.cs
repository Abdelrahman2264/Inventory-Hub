using InventorySystem.ViewModels;

namespace InventorySystem.Repository.IRepository
{
    public interface IAssignmentRepository
    {
        public Task<Assignment>AssignToAsync(Assignment assignment);
        public Task<bool> ReturnFromAsync(int id);
        public Task<IEnumerable<Assignment>> GetAllAssignments();
        public Assignment GetByIdAsync(int id);
        public Task<IEnumerable<Assignment>> GetAssignmetsByIdAsync(int id);
        public Task<bool> isUsed(int id);
        public Task<IEnumerable<Assignment>> GetAllAssignmentsReturned();

    }
}
