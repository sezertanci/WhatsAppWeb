using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class GroupMessage : BaseEntity
    {
        [Required]
        public Guid GroupUserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Boş geçemezsiniz.")]
        public string Message { get; set; }

        public virtual GroupUser GroupUser { get; set; }

        public GroupMessage()
        {

        }

        public GroupMessage(Guid groupUserId, string message)
        {
            GroupUserId = groupUserId;
            Message = message;
        }
    }
}
