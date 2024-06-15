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

        public async Task<Plate> GetUnreservedPlate(string registration)
        {
            return await _context.Plates.Where(p => p.Registration == registration && p.Reserved == false ).FirstAsync();
        }

        public void AddPlate(PlateDto plate)
        {
            _context.Plates.Add(PlateMapper.Map(plate));
            _context.SaveChanges();

        }

        public Task<Plate> GetPlate(string registration)
        {
            return _context.Plates.Where(x => x.Registration == registration && x.Reserved == false).FirstAsync();
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
