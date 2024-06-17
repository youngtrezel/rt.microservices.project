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

        public Plate GetPlate(string registration)
        {
            return _plateRepository.GetPlate(registration);
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
            var plates = await _plateRepository.GetUnsoldPlates(pageNumber, pageSize);

            return plates;
        }

        public bool AddPlate(PlateDto plate)
        {
            return _plateRepository.AddPlate(plate);
        }

        public bool UpdatePlate(Plate plate)
        {                      
            return _plateRepository.UpdatePlate(plate);
        }

        public Plate ReservePlate(Plate plate)
        {
            plate.Reserved = true;
            _plateRepository.UpdatePlate(plate);

            return plate;
        }
    }
}
