using Sales.Domain.Models;
using Sales.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sales.Infrastructure.Data;

namespace Sales.Repository
{
    public class PlateRepository : IPlateRepository
    {
        private readonly ApplicationDbContext _context;

        public PlateRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize)
        {
            var plates = await _context.Plates.AsNoTracking()
                .Where(x => x.Reserved == false)
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return plates;
        }


        public async Task<Plate> UpdatePlate(Plate plate)
        {
            var updatePlate = _context.Plates.First(p => p.Id == plate.Id);
            if (updatePlate != null)
            {
                _context.Entry(updatePlate).CurrentValues.SetValues(plate);
                await _context.SaveChangesAsync();

                return updatePlate;
            }

            return plate;
        }


    }
}
