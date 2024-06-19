namespace EventBus.Constants
{
    public class QueuesConsts
    {
        // events
        public const string PlateAddedEventQueueName = "plate-added-queue";
        public const string PlateReservedEventQueueName = "plate-reserved-queue";
        public const string PlateUnreservedEventQueueName = "plate-unreserved-queue";
        public const string PlateSoldEventQueueName = "plate-sold-queue";

        // messages
        public const string AddPlateMessageQueueName = "add-plate-message-queue";
        public const string ReservePlateMessageQueueName = "reserve-plate-message-name";
        public const string UnreservePlateMessageQueueName = "unreserve-plate-message-queue";
        public const string SellPlateMessageQueueName = "sell-plate-message-queue";
        
    }
}
