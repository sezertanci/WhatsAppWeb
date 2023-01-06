namespace Common.Models.ViewModels
{
    public class ChatMessageViewModel
    {
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public Guid ChatId { get; set; }
        public Guid UserChatId { get; set; }
        public string Message { get; set; }
        public DateTime SendedDate { get; set; }
        public bool IsMyMessage { get; set; }
        public bool HasBeenRead { get; set; }
    }
}
