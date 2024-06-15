using Sales.Domain.Models;
using Sales.Domain.Models.Data;

namespace Sales.API.Interfaces
{
    public interface IPlatesHandler
    {
        public Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize);

        public Plate SellPlate(Plate plate, decimal soldPrice);
    }
}
