using EventBus.Events.Interfaces;
using Marketing.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.Infrastructure.Consumers
{
    public class PlateReservedEventConsumer : IConsumer<IPlateReservedEvent>
    {
        private readonly ApplicationDbContext _context;

        public PlateReservedEventConsumer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<IPlateReservedEvent> context)
        {
            if (context.Message.Id == Guid.Empty)
            {
                return;
            }

            var plateExists = await _context.Plates.AnyAsync(x => x.Id == context.Message.Id);

            if (plateExists)
            {
                var updatePlate = await _context.Plates.Where(x => x.Id == context.Message.Id).FirstOrDefaultAsync();
                updatePlate.Reserved = true;
                await _context.SaveChangesAsync();
            }

            return;
        }
    }
}
