using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UserChat : BaseEntity
    {
        [Required]
        public Guid ChatId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserChatMessage> UserChatMessages { get; set; }

        public UserChat()
        {

        }

        public UserChat(Guid chatId, Guid userId)
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
