using Sales.API.Interfaces;
using Sales.Domain.Models;
using Sales.Domain.Models.Data;
using Sales.Repository.Interfaces;

namespace Sales.API.Handlers
{
    public class PlatesHandler : IPlatesHandler
    {
        private readonly IPlateRepository _plateRepository;



        public PlatesHandler(IPlateRepository plateRepository)
        {
            _plateRepository = plateRepository;
        }

        public async Task<int> GetPlateCount(string filter)
        {
            var plateCount = await _plateRepository.GetAvailablePlateCount(filter);

            return plateCount;
        }

        public async Task<int> GetSoldPlateCount()
        {
            var plateCount = await _plateRepository.GetSoldPlateCount();

            return plateCount;
        }

        public async Task<IEnumerable<Plate>> GetSoldPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetSoldPlates(pageNumber, pageSize);

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetPlates(pageNumber, pageSize);

            return plates;
        }

        public async Task<Plate> SellPlate(string registration)
        {
            var plate = await _plateRepository.GetPlate(registration);

            if (plate == null)
            {
                var emptyPlate = new Plate();
                return emptyPlate;
            }

            plate.Sold = true;

            await _plateRepository.UpdatePlate(plate);

            return plate;
        }
        public async Task<decimal> GetRevenue()
        {
            return await _plateRepository.GetRevenue();
        }
    }
}
