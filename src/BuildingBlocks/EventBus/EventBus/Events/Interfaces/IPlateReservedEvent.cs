using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events.Interfaces
{
    public interface IPlateReservedEvent
    {
        public Guid Id { get; set; }
    }
}
