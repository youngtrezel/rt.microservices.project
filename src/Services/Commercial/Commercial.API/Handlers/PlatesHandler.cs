using Commercial.API.Interfaces;
using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;
using Commercial.Repository.Interfaces;

namespace Commercial.API.Handlers
{
    public class PlatesHandler : IPlatesHandler
    {
        private readonly IPlateRepository _plateRepository;

        public PlatesHandler(IPlateRepository plateRepository)
        {
            _plateRepository = plateRepository;
        }

        public async Task<Plate> GetPlate(string registration)
        {
            return await _plateRepository.GetPlate(registration);
        }

        public async Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetPlates(pageNumber, pageSize);

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetUnreservedPlates(pageNumber, pageSize);

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetUnsoldPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetPlates(pageNumber, pageSize);

            return plates;
        }

        public async Task<Plate?> AddPlate(PlateDto plate)
        {
            return await _plateRepository.AddPlate(plate);
        }

        public async Task<Plate?> UpdatePlate(Plate plate)
        {                      
            return await _plateRepository.UpdatePlate(plate);
        }

        public async Task<Plate?> ReservePlate(string registration)
        {
            var plate = await _plateRepository.GetPlate(registration);

            if(plate == null)
            {
                var emptyPlate = new Plate();
                return emptyPlate;
            }

            plate.Reserved = true;

            await _plateRepository.UpdatePlate(plate);

            return plate;
        }

        public async Task<Plate?> UnreservePlate(string registration)
        {
            var plate = await _plateRepository.GetPlate(registration);

            if (plate == null)
            {
                var emptyPlate = new Plate();
                emptyPlate.Reserved = true;
                return emptyPlate;
            }

            plate.Reserved = false;

            await _plateRepository.UpdatePlate(plate);

            return plate;
        }

        public async Task<int> GetAvailablePlateCount(string filter)
        {
            return await _plateRepository.GetAvailablePlateCount(filter);
        }

        public async Task<IEnumerable<Plate>> GetFilteredUnsold(string letters, int pageNumber, int pageSize)
        {
            return await _plateRepository.GetFilteredUnsold(letters, pageNumber, pageSize);
        }
    }
}
