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

        public void AddPlate(PlateDto plate)
        {
            _context.Plates.Add(PlateMapper.Map(plate));
            _context.SaveChanges();

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

        public async Task<IEnumerable<Plate>> GetFilteredPlates(string letters)
        {
            IEnumerable<Plate> plates = [];

            if (!string.IsNullOrEmpty(letters))
            {
                int num;

                if (int.TryParse(letters, out num))
                {
                    plates = await _context.Plates.AsNoTracking()
                        .Where(x => num == x.Numbers)
                        .OrderByDescending(x => x.SalePrice)
                        .ToListAsync();
                }
                else
                {
                    plates = await _context.Plates.AsNoTracking()
                        .Where(x => letters == x.Letters)
                        .OrderByDescending(x => x.SalePrice)
                        .ToListAsync();
                }
            }

            return plates;
        }
    }
}
