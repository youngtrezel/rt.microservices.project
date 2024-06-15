using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commercial.Repository.Interfaces
{
    public interface IPlateRepository
    {
        public void AddPlate(PlateDto plate);

        public Task<Plate> GetPlate(string registration);

        public Task<Plate> UpdatePlate(Plate plate);

        public Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize);

        public Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize);

        public Task<IEnumerable<Plate>> GetUnsoldPlates(int pageNumber, int pageSize);
    }
}
