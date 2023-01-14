using Application.Interfaces.Repositories;
using Common.Models.RequestModels.ChatRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.GroupMessageCommand.Delete
{
    public class DeleteGroupMessageCommandHandler : IRequestHandler<DeleteGroupMessageCommand, bool>
    {
        private readonly IGroupUserRepository groupUserRepository;
        private readonly IGroupMessageRepository groupMessageRepository;

        public DeleteGroupMessageCommandHandler(IGroupUserRepository groupUserRepository, IGroupMessageRepository groupMessageRepository)
        {
            this.groupUserRepository = groupUserRepository;
            this.groupMessageRepository = groupMessageRepository;
        }

        public async Task<bool> Handle(DeleteGroupMessageCommand request, CancellationToken cancellationToken)
        {
            List<GroupUser> groupUsers = await groupUserRepository.GetListAsync(x => x.GroupId == request.GroupId && !x.Deleted, includes: x => x.GroupMessages);

            List<GroupMessage> groupMessages = new();

            foreach(var groupUser in groupUsers)
            {
                foreach(var groupMessage in groupUser.GroupMessages.Where(x => x.ShowToUsers != null && x.ShowToUsers.Contains(request.UserId.ToString())))
                {
                    groupMessage.ShowToUsers = groupMessage.ShowToUsers.Replace(request.UserId.ToString(), "").Replace(",,", ",");

                    groupMessage.ShowToUsers = groupMessage.ShowToUsers == "," ? "" : groupMessage.ShowToUsers;

                    groupMessages.Add(groupMessage);
                }
            }

            if(groupMessages.Count > 0)
                await groupMessageRepository.UpdateRangeAsync(groupMessages);

            return true;
        }
    }
}
