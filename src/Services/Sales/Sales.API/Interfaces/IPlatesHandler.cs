using Sales.Domain.Models;
using Sales.Domain.Models.Data;

namespace Sales.API.Interfaces
{
    public interface IPlatesHandler
    {
        public Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize);

        public Task<int> GetPlateCount(string filter);

        public Task<int> GetSoldPlateCount();

        public Task<IEnumerable<Plate>> GetSoldPlates(int pageNumber, int pageSize);

        public Task<Plate> SellPlate(string registration);
    }
}
