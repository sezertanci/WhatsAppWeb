using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Group : BaseEntity
    {
        [Required(ErrorMessage = "Boş geçemezsiniz.")]
        public Guid UserId { get; set; } //Grubu oluşturan kullanıcı

        [Required(AllowEmptyStrings = false, ErrorMessage = "Boş geçemezsiniz.")]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }

        public Group()
        {

        }

        public Group(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}
