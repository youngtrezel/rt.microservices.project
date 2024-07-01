using Marketing.API.Interfaces;
using Marketing.Domain.Models;
using Marketing.Domain.Models.Data;
using Marketing.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize, bool ascending)
        {
            var plates = await _plateRepository.GetPaginationPlates(pageNumber, pageSize, ascending);

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetFilteredPlates(string letters, int pageNumber, int pageSize, bool ascending)
        {
            var plates = await _plateRepository.GetFilteredPlates(letters, pageNumber, pageSize, ascending);

            return plates;
        }

        public async Task<int> GetFilteredPlatesCount(string letters)
        {
            var plates = await _plateRepository.GetFilteredPlatesCount(letters);

            return plates;
        }



    }
}
