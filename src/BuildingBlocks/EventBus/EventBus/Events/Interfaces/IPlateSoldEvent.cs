using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events.Interfaces
{
    public interface IPlateSoldEvent
    {
        public Guid Id { get; set; }
    }
}
