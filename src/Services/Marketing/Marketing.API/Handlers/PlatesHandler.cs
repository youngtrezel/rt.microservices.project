using Marketing.API.Interfaces;
using Marketing.Domain.Models;
using Marketing.Domain.Models.Data;
using Marketing.Repository.Interfaces;

namespace Marketing.API.Handlers
{
    public class PlatesHandler : IPlatesHandler
    {
        private readonly IPlateRepository _plateRepository;

        public PlatesHandler(IPlateRepository plateRepository)
        {
            _plateRepository = plateRepository;
        }

        public async Task<Plate?> SellPlate(string registration)
        {
            var plate = await _plateRepository.GetPlate(registration);

            if(plate == null)
            {
                var emptyPlate = new Plate();
                return emptyPlate;
            }

            plate.Sold = true;

            await _plateRepository.UpdatePlate(plate);

            return plate;
        }

        public async Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetPlates(pageNumber, pageSize);

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetFilteredPlates(string letters, int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetFilteredPlates(letters, pageNumber, pageSize);

            return plates;
        }

    }
}
