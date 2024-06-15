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


        public async Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize)
        {
            var plates = await _plateRepository.GetPlates(pageNumber, pageSize);

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetFilteredPlates(string letters)
        {
            var plates = await _plateRepository.GetFilteredPlates(letters);

            return plates;
        }

    }
}
