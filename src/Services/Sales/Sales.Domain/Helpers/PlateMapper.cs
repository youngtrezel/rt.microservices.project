using Sales.Domain.Models;
using Sales.Domain.Models.Data;

namespace Sales.Domain.Helpers
{
    public static class PlateMapper
    {
        public static Plate Map(PlateDto plate)
        {
            return new Plate
            {
                Id = Guid.NewGuid(),
                Letters = plate.Letters,
                Numbers = plate.Numbers,
                PurchasePrice = plate.PurchasePrice,
                Registration = plate.Registration,
                SalePrice = plate.SalePrice,
                DateSold = plate.DateSold,
                Sold = plate.Sold,
                PriceSoldFor = plate.PriceSoldFor,
                Reserved = plate.Reserved,
            };
        }
    }
}
