using Commercial.Domain.Helpers;
using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;
using Commercial.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Commercial.Infrastructure.Data;
using System.Linq.Expressions;

namespace Commercial.Repository
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

        public async Task<Plate> GetUnreservedPlate(string registration)
        {
            return await _context.Plates.Where(p => p.Registration == registration && p.Reserved == false ).FirstAsync();
        }

        public async Task<Plate?> AddPlate(PlateDto plate)
        {
            Plate plateToAdd = PlateMapper.Map(plate);

            await _context.Plates.AddAsync(plateToAdd);
            await _context.SaveChangesAsync();

            return _context.Plates.Where(x => x.Id == plateToAdd.Id).FirstOrDefaultAsync().Result;

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
            var platesFiltered = await _context.Plates.Where(x => x.Sold == false)
                .Select(t => t).ToListAsync();

            var pagedResults = platesFiltered
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return pagedResults;
        }

        public async Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize)
        {
            var platesFiltered = await _context.Plates.Where(x => x.Reserved == false && x.Sold == false)
                .Select(t => t).ToListAsync();
                
            var pagedResults = platesFiltered
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return pagedResults;
        }

        public async Task<IEnumerable<Plate>> GetUnsoldPlates(int pageNumber, int pageSize)
        {
            var platesFiltered = await _context.Plates.Where(x => x.Sold == false)
                .Select(t => t).ToListAsync();

            var pagedResults = platesFiltered
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return pagedResults;
        }

        public async Task<IEnumerable<Plate>> GetFilteredUnsold(string letters, int pageNumber, int pageSize)
        {
            IEnumerable<Plate> plates = [];

            if (!string.IsNullOrEmpty(letters))
            {
                int num;

                if (int.TryParse(letters, out num))
                {
                    plates = await _context.Plates.AsNoTracking()
                        .Where(x => x.Numbers.ToString().Contains(letters) && x.Sold == false)
                        .OrderByDescending(x => x.SalePrice)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }
                else
                {
                    plates = await _context.Plates.AsNoTracking()
                        .Where(x => x.Letters.Contains(letters) && x.Sold == false)
                        .OrderByDescending(x => x.SalePrice)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }
            }

            return plates;
        }

        public async Task<int> GetAvailablePlateCount(string filter)
        {
            switch (filter)
            {
                case "unreserved":
                    return await _context.Plates.Where(p => p.Sold == false && p.Reserved == false).CountAsync();
                    break;
                case "unsold":
                    return await _context.Plates.Where(p => p.Sold == false ).CountAsync();
                    break;
                default:
                    return 0;
                    break;
            }
        }
    }
}
