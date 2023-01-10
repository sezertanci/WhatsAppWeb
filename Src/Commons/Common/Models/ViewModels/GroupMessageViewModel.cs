namespace Common.Models.ViewModels
{
    public class GroupMessageViewModel
    {
        public ChatMessageViewModel ChatMessageViewModel { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
