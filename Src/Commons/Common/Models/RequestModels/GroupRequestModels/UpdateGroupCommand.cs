using MediatR;

namespace Common.Models.RequestModels.GroupRequestModels
{
    public class UpdateGroupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public UpdateGroupCommand()
        {

        }

        public UpdateGroupCommand(Guid ıd, Guid userId, string name)
        {
            Id = ıd;
            UserId = userId;
            Name = name;
        }
    }
}
