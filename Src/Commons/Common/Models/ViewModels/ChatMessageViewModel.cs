namespace Common.Models.ViewModels
{
    public class ChatMessageViewModel
    {
        public Guid SenderUserId { get; set; }
        public string UserName { get; set; }
        public Guid ChatId { get; set; }
        public string Message { get; set; }
        public DateTime SendedDate { get; set; }
        public bool IsMyMessage { get; set; }
        public bool HasBeenRead { get; set; }
    }
}
