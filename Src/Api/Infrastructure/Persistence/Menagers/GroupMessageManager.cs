using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Common.Models.ViewModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Menagers
{
    public class GroupMessageManager : IGroupMessageService
    {
        private readonly IGroupUserRepository groupUserRepository;
        private readonly IGroupMessageRepository groupMessageRepository;

        public GroupMessageManager(IGroupUserRepository groupUserRepository, IGroupMessageRepository groupMessageRepository)
        {
            this.groupUserRepository = groupUserRepository;
            this.groupMessageRepository = groupMessageRepository;
        }

        public async Task<List<ChatMessageViewModel>> GetChatMessagesAsync(Guid userId, Guid groupId)
        {
            IQueryable<GroupUser> groupUserQuery = groupUserRepository.Query();

            List<GroupUser> groupUsers = await groupUserQuery.Where(x => x.GroupId == groupId)
                 .Include(x => x.GroupMessages)
                 .Include(x => x.User).ToListAsync();

            List<ChatMessageViewModel> chatMessageViewModels = new();

            foreach(var groupUser in groupUsers)
            {
                foreach(var groupMessage in groupUser.GroupMessages.Where(x => !x.Deleted && x.ShowToUsers != null && x.ShowToUsers.Contains(userId.ToString())))
                {
                    ChatMessageViewModel chatMessageViewModel = new()
                    {
                        IsMyMessage = userId == groupUser.UserId,
                        Message = groupMessage.Message,
                        SendedDate = groupMessage.CreatedDate,
                        SenderUserId = groupUser.UserId,
                        UserName = groupUser.User.Name,
                        ChatId = groupId
                    };

                    chatMessageViewModels.Add(chatMessageViewModel);
                }
            }

            return chatMessageViewModels.OrderBy(x => x.SendedDate).ToList();
        }

        public async Task<GroupMessageViewModel> SendMessageAsync(SendedGroupMessageViewModel sendedGroupMessageViewModel)
        {
            List<GroupUser> groupUsers = await groupUserRepository.GetListAsync(x => x.GroupId == sendedGroupMessageViewModel.GroupId, includes: x => x.User);

            GroupUser groupUser = groupUsers.FirstOrDefault(x => x.UserId == sendedGroupMessageViewModel.UserId);

            if(groupUser == null)
                return null;

            GroupMessage groupMessage = new()
            {
                GroupUserId = groupUser.Id,
                Message = sendedGroupMessageViewModel.Message,
                ShowToUsers = CombiningUserIds(groupUsers.Select(x => x.UserId).ToList())
            };

            await groupMessageRepository.AddAsync(groupMessage);

            GroupMessageViewModel groupMessageViewModel = new()
            {
                ChatMessageViewModel = new()
                {
                    Message = sendedGroupMessageViewModel.Message,
                    SendedDate = groupMessage.CreatedDate,
                    SenderUserId = sendedGroupMessageViewModel.UserId,
                    ChatId = sendedGroupMessageViewModel.GroupId,
                    UserName = groupUser.User.Name
                },
                UserIds = groupUsers.Select(x => x.UserId).ToList()
            };

            return groupMessageViewModel;
        }

        string CombiningUserIds(List<Guid> userIds)
        {
            string result = "";

            foreach(var userId in userIds)
            {
                result += userId + ",";
            }

            result = result[..^1];

            return result;
        }
    }
}
