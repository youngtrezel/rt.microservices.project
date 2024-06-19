using EventBus.Events.Interfaces;
using Sales.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;


namespace Sales.Infrastructure.Consumers
{
    public class PlateSoldEventConsumer : IConsumer<IPlateSoldEvent>
    {
        private readonly ApplicationDbContext _context;

        public PlateSoldEventConsumer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<IPlateSoldEvent> context)
        {
            if (context.Message.Id == Guid.Empty)
            {
                return;
            }

            var plateExists = await _context.Plates.AnyAsync(x => x.Id == context.Message.Id);

            if (plateExists)
            {
                var updatePlate = await _context.Plates.Where(x => x.Id == context.Message.Id).FirstOrDefaultAsync();
                updatePlate.Sold = true;
                await _context.SaveChangesAsync();
            }

            return;
        }
    }
}
