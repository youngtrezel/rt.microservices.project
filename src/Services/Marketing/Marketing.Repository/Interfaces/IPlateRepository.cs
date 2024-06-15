
using Marketing.Domain.Models;

namespace Marketing.Repository.Interfaces
{
    public interface IPlateRepository
    {
        public Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize);

        public Task<IEnumerable<Plate>> GetFilteredPlates(string letters);
    }
}
