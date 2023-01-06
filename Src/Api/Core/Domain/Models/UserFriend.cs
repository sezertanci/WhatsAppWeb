using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UserFriend : BaseEntity
    {
        public Guid UserId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Boş geçemezsiniz.")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "10 karakter girmelisiniz.")]
        public string PhoneNumber { get; set; }

        public virtual User User { get; set; }
    }
}
