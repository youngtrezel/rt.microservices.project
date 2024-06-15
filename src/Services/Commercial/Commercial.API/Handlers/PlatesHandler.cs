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
            var plates = await _plateRepository.GetUnsoldPlates(pageNumber, pageSize);

            return plates;
        }

        public void AddPlate(PlateDto plate)
        {
            _plateRepository.AddPlate(plate);
        }

        public Plate UpdatePlate(Plate plate)
        {
            _plateRepository.UpdatePlate(plate); 
            
            return plate;
        }

        public Plate ReservePlate(Plate plate)
        {
            plate.Reserved = true;
            _plateRepository.UpdatePlate(plate);

            return plate;
        }
    }
}
