using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;

namespace Commercial.Domain.Helpers
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
