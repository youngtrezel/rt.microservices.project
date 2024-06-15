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

        public async Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetPlates(pageNumber, pageSize);

            return plates;
        }

        public Plate SellPlate(Plate plate, decimal soldPrice)
        {
            plate.DateSold = DateTime.Now;
            plate.PriceSoldFor = soldPrice;
            plate.Sold = true;
            plate.Reserved = false;

            _plateRepository.UpdatePlate(plate);

            return plate;
        }
    }
}
