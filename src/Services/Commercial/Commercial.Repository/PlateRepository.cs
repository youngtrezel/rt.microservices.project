using Commercial.Domain.Helpers;
using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;
using Commercial.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Commercial.Infrastructure.Data;

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
            var plates = await _context.Plates.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return plates;
        }

        public async Task<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize)
        {
            var platesFiltered = await _context.Plates.Where(x => x.Reserved == false)
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
    }
}
