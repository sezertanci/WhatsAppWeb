namespace Domain.Models
{
    public class Chat : BaseEntity
    {
        public virtual ICollection<UserChat> UserChats { get; set; }

        public Chat()
        {

        }
    }
}
