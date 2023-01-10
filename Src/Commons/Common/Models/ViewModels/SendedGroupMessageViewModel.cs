namespace Common.Models.ViewModels
{
    public class SendedGroupMessageViewModel
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string Message { get; set; }
    }
}
