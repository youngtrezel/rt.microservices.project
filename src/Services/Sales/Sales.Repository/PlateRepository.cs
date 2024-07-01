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

        public async Task<Plate?> GetPlate(string registration)
        {
            return await _context.Plates.Where(x => x.Registration == registration).Select(x => x).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize)
        {
            IQueryable<Plate> platesQuery = _context.Plates.Where(x => x.Reserved == false && x.Sold == false);

            var pagedResults = platesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return pagedResults;
        }

        public async Task<IEnumerable<Plate>> GetSoldPlates(int pageNumber, int pageSize)
        {
            IQueryable<Plate> platesQuery = _context.Plates.Where(x => x.Sold == true);

            var pagedResults = platesQuery
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
                _context.SaveChanges();

                return updatePlate;
            }

            return plate;
        }

        public async Task<int> GetAvailablePlateCount(string filter)
        {

            try
            {
                switch (filter)
                {
                    case "unsold":

                        IQueryable<Plate> unreservedQuery = _context.Plates.Where(p => p.Sold == false);

                        return await unreservedQuery.CountAsync();

                    case "unreserved":

                        IQueryable<Plate> soldQuery = _context.Plates.Where(p => p.Sold == false && p.Reserved == false);

                        var count = await soldQuery.CountAsync();

                        return count;

                    default:
                        return 0;
                        ;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
            
        }

        public async Task<int> GetSoldPlateCount()
        {
            try
            {
                IQueryable<Plate> unreservedQuery = _context.Plates.Where(p => p.Sold == true);

                return await unreservedQuery.CountAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<decimal> GetRevenue()
        {
            try
            {
                IQueryable<decimal> salePricesQuery = _context.Plates.Where(p => p.Sold == true).Select(x => x.SalePrice); ;

                decimal result = await salePricesQuery.SumAsync();

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
