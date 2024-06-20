using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;

namespace Commercial.Repository.Interfaces
{
    public interface IPlateRepository
    {
        public Task<Plate?> AddPlate(PlateDto plate);

        public Task<Plate?> GetPlate(string registration);

        public Task<Plate?> UpdatePlate(Plate plate);

        public Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize);

        public Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize);

        public Task<IEnumerable<Plate>> GetUnsoldPlates(int pageNumber, int pageSize);

        public Task<int> GetAvailablePlateCount(string filter);

        public Task<IEnumerable<Plate>> GetFilteredUnsold(string letters, int pageNumber, int pageSize);
    }
}
