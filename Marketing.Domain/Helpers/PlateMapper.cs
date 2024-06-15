using Marketing.Domain.Models;
using Marketing.Domain.Models.Data;

namespace Marketing.Domain.Helpers
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
