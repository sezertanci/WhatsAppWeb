using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UserChatMessage : BaseEntity
    {
        [Required]
        public Guid UserChatId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Boş geçemezsiniz.")]
        public string Message { get; set; }

        public bool HasBeenRead { get; set; }

        [MaxLength(100)]
        public string ShowToUsers { get; set; }

        public virtual UserChat UserChat { get; set; }

        public UserChatMessage()
        {

        }

        public UserChatMessage(Guid userChatId, string message, bool hasBeenRead)
        {
            UserChatId = userChatId;
            Message = message;
            HasBeenRead = hasBeenRead;
        }
    }
}
