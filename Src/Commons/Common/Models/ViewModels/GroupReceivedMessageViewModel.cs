namespace Common.Models.ViewModels
{
    public class GroupReceivedMessageViewModel
    {
        public Guid GroupUserId { get; set; }
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime SendedDate { get; set; }
        public bool IsMyMessage { get; set; }
    }
}
