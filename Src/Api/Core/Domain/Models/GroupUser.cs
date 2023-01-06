using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class GroupUser : BaseEntity
    {
        [Required]
        public Guid GroupId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }

        public GroupUser()
        {

        }

        public GroupUser(Guid groupId, Guid userId)
        {
            GroupId = groupId;
            UserId = userId;
        }
    }
}
