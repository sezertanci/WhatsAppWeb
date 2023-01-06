using MediatR;

namespace Common.Models.RequestModels.GroupRequestModels
{
    public class CreateGroupCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<Guid> UserIds { get; set; } //Gruba eklenecek kullanıcılar

        public CreateGroupCommand()
        {

        }

        public CreateGroupCommand(Guid userId, string name, List<Guid> userIds)
        {
            UserId = userId;
            Name = name;
            UserIds = userIds;
        }
    }
}
