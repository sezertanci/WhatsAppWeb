namespace Common.Models.ViewModels
{
    public class SendedMessageViewModel
    {
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public string Message { get; set; }
    }
}
