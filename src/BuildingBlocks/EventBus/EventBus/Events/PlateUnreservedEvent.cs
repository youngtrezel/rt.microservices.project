using EventBus.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class PlateUnreservedEvent : IPlateUnreservedEvent
    {
        public Guid Id { get; set; }
    }
}
