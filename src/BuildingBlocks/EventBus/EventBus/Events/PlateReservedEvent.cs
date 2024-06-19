using EventBus.Events.Interfaces;

namespace EventBus.Events
{
    public class PlateReservedEvent : IPlateReservedEvent
    {
        public Guid Id { get; set; }
    }
}
