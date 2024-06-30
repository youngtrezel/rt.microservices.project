using Marketing.Domain.Helpers;
using Marketing.Domain.Models;
using Marketing.Domain.Models.Data;
using Marketing.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Marketing.Infrastructure.Data;
using EventBus.Events.Interfaces;

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

        public async Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize, bool ascending)
        {
            IQueryable<Plate> platesQuery = _context.Plates.Where(x => x.Sold == false);

            var plates = platesQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (ascending)
            {
                return await plates.OrderBy(x => x.SalePrice).ToListAsync();
            } else
            {
                return await plates.OrderByDescending(x => x.SalePrice).ToListAsync();
            }

        }

        public async Task<IEnumerable<Plate>> GetFilteredPlates(string letters, int pageNumber, int pageSize, bool ascending)
        {
            int num;

            if (int.TryParse(letters, out num))
            {
                IQueryable<Plate> platesQuery = _context.Plates.Where(x => x.Numbers.ToString().Contains(letters));

                var plates = platesQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                if (ascending)
                {
                    return await plates.OrderBy(x => x.SalePrice).ToListAsync();
                }
                else
                {
                    return await plates.OrderByDescending(x => x.SalePrice).ToListAsync();
                }
            }
            else
            {
                IQueryable<Plate> platesQuery = _context.Plates.Where(x => x.Letters.Contains(letters));

                var plates = platesQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                if (ascending)
                {
                    return await plates.OrderBy(x => x.SalePrice).ToListAsync();
                }
                else
                {
                    return await plates.OrderByDescending(x => x.SalePrice).ToListAsync();
                }

            }
        }

        public async Task<int> GetAvailablePlateCount(string filter)
        {
            switch (filter)
            {
                case "sold":

                    IQueryable<Plate> unreservedQuery = _context.Plates.Where(p => p.Sold == false);

                    return await unreservedQuery.CountAsync();
                    
                case "unreserved":

                    IQueryable<Plate> soldQuery = _context.Plates.Where(p => p.Sold == false && p.Reserved == false);

                    return await soldQuery.CountAsync();
                    
                default:
                    return 0;
                    
            }
        }

        public async Task<int> GetFilteredPlatesCount(string letters)
        {
            int num;

            if (int.TryParse(letters, out num))
            {
                IQueryable<Plate> platesQuery = _context.Plates.Where(x => x.Numbers.ToString().Contains(letters) && x.Sold == false);

                var platesCount = await platesQuery.CountAsync();

                return platesCount;
            }
            else
            {
                IQueryable<Plate> platesQuery = _context.Plates.Where(x => x.Letters.ToString().Contains(letters) && x.Sold == false);

                var platesCount = await platesQuery.CountAsync();

                return platesCount;
            }
        }
    }
}
