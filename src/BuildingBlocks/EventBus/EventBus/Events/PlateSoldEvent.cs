using EventBus.Events.Interfaces;


namespace EventBus.Events
{
    public class PlateSoldEvent : IPlateSoldEvent
    {
        public Guid Id { get; set; }
    }
}
