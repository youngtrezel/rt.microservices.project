using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;

namespace Commercial.API.Interfaces
{
    public interface IPlatesHandler
    {
        public Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize);

        public Task<Plate> GetPlate(string registration);

        public Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize);

        public void AddPlate(PlateDto plate);

        public Plate UpdatePlate(Plate plate);

        public Task<IEnumerable<Plate>> GetUnsoldPlates(int pageNumber, int pageSize);

        public Plate ReservePlate(Plate plate);
    }
}
