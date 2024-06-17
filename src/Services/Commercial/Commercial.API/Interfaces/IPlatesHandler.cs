using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;

namespace Commercial.API.Interfaces
{
    public interface IPlatesHandler
    {
        public Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize);

        public Plate GetPlate(string registration);

        public Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize);

        public bool AddPlate(PlateDto plate);

        public bool UpdatePlate(Plate plate);

        public Task<IEnumerable<Plate>> GetUnsoldPlates(int pageNumber, int pageSize);

        public Plate ReservePlate(Plate plate);
    }
}
