using Marketing.Domain.Helpers;
using Marketing.Domain.Models;
using Marketing.Domain.Models.Data;
using Marketing.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Marketing.Infrastructure.Data;

namespace Marketing.Repository
{
    public class PlateRepository : IPlateRepository
    {
        private readonly ApplicationDbContext _context;

        public PlateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Plate?> GetPlate(string registration)
        {
            return await _context.Plates.Where(x => x.Registration == registration).Select(x => x).FirstOrDefaultAsync();
        }


        public async Task<Plate?> UpdatePlate(Plate plate)
        {
            var updatePlate = await _context.Plates.FirstAsync(p => p.Id == plate.Id);
            _context.Entry(updatePlate).CurrentValues.SetValues(plate);
            await _context.SaveChangesAsync();

            var savedPlate = await _context.Plates.Where(x => x.Id == updatePlate.Id).FirstOrDefaultAsync();

            if (savedPlate == updatePlate)
            {
                return savedPlate;
            }

            return plate;
        }

        public async Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize)
        {
            var plates = await _context.Plates.AsNoTracking()
                .OrderByDescending(x => x.SalePrice)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetFilteredPlates(string letters, int pageNumber, int pageSize)
        {
            IEnumerable<Plate> plates = [];

            if (!string.IsNullOrEmpty(letters))
            {
                int num;

                if (int.TryParse(letters, out num))
                {
                    plates = await _context.Plates.AsNoTracking()
                        .Where(x => x.Numbers.ToString().Contains(letters))
                        .OrderByDescending(x => x.SalePrice)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }
                else
                {
                    plates = await _context.Plates.AsNoTracking()
                        .Where(x => x.Letters.Contains(letters))
                        .OrderByDescending(x => x.SalePrice)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }
            }

            return plates;
        }
    }
}
