namespace Common.Models.ViewModels
{
    public class ChatViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public DateTime SendedDate { get; set; }
        public bool IsMyMessage { get; set; }
        public bool HasBeenRead { get; set; }
        public bool IsGroup { get; set; }
        public string UserName { get; set; }
        public bool IsMyGroup { get; set; }
    }
}
