
using Marketing.Domain.Models;

namespace Marketing.Repository.Interfaces
{
    public interface IPlateRepository
    {
        public Task<Plate?> GetPlate(string registration);

        public Task<Plate?> UpdatePlate(Plate plate);
        public Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize, bool ascending);

        public Task<IEnumerable<Plate>> GetFilteredPlates(string letters, int pageNumber, int pageSize, bool ascending);

        public Task<int> GetAvailablePlateCount(string filter);

        public Task<int> GetFilteredPlatesCount(string letters);

        
    }
}
