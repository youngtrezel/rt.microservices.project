using Sales.Domain.Models;

namespace Sales.Repository.Interfaces
{
    public interface IPlateRepository
    {
        public Task<Plate?> GetPlate(string registration);
        public Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize);

        public Task<Plate> UpdatePlate(Plate plate);

        public Task<int> GetAvailablePlateCount(string filter);

        public Task<int> GetSoldPlateCount();

        public Task<IEnumerable<Plate>> GetSoldPlates(int pageNumber, int pageSize);
    }
}
