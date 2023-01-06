using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User : BaseEntity
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Boş geçemezsiniz.")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "10 karakter girmelisiniz.")]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Boş geçemezsiniz.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Boş geçemezsiniz.")]
        [MaxLength(50)]
        public string Password { get; set; }

        public virtual ICollection<UserChat> UserChats { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<UserFriend> UserFriends { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public User()
        {

        }

        public User(string phoneNumber, string name, string password)
        {
            PhoneNumber = phoneNumber;
            Name = name;
            Password = password;
        }
    }
}
