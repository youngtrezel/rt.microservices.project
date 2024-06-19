using EventBus.Events.Interfaces;
using Marketing.Domain.Models;
using Marketing.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Marketing.Infrastructure.Consumers
{
    public class PlateAddedEventConsumer : IConsumer<IPlateAddedEvent>
    {

        private readonly ApplicationDbContext _context;

        public PlateAddedEventConsumer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<IPlateAddedEvent> context)
        {
            if(context.Message.Id == Guid.Empty)
            {
                return;
            }

            var plateExists = await _context.Plates.AnyAsync(x => x.Id == context.Message.Id);
            if(plateExists)
            {
                return;
            }

            Plate plate = new()
            {
                Id = context.Message.Id,
                Registration = context.Message.Registration,
                Letters = context.Message.Letters,
                Numbers = context.Message.Numbers,
                SalePrice = context.Message.SalePrice,
                DateSold = context.Message.DateSold,
                Sold = context.Message.Sold,
                Reserved = context.Message.Reserved,
                PriceSoldFor = context.Message.PriceSoldFor,
                PurchasePrice = context.Message.PurchasePrice
            };

            await _context.Plates.AddAsync(plate);
            await _context.SaveChangesAsync();
        }
    }
}
