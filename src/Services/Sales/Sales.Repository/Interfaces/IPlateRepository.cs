using Sales.Domain.Models;

namespace Sales.Repository.Interfaces
{
    public interface IPlateRepository
    {
        public Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize);

        public Task<Plate> UpdatePlate(Plate plate);

    }
}
