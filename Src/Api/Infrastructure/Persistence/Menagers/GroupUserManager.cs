using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Common.Models.ViewModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Persistence.Menagers
{
    public class GroupUserManager : IGroupUserService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IGroupUserRepository groupUserRepository;
        private readonly IGroupMessageRepository groupMessageRepository;
        private readonly IUserFriendRepository userFriendRepository;

        public GroupUserManager(IGroupRepository groupRepository, IGroupUserRepository groupUserRepository, IGroupMessageRepository groupMessageRepository, IUserFriendRepository userFriendRepository)
        {
            this.groupRepository = groupRepository;
            this.groupUserRepository = groupUserRepository;
            this.groupMessageRepository = groupMessageRepository;
            this.userFriendRepository = userFriendRepository;
        }

        public async Task<List<ChatViewModel>> UserGroups(Guid userId)
        {
            List<GroupUser> userGroups = await groupUserRepository.GetListAsync(x => x.UserId == userId && !x.Deleted, true, null, x => x.Group, y => y.User);

            List<GroupUser> friendUserGroups = await groupUserRepository.GetListAsync(x => userGroups.Select(y => y.GroupId).Contains(x.GroupId) && x.UserId != userId, includes: y => y.User);

            List<Guid> userGroupIds = new();
            userGroupIds.AddRange(userGroups.Select(x => x.Id));
            userGroupIds.AddRange(friendUserGroups.Select(x => x.Id));

            List<GroupUser> userGroupsWithMessage = groupUserRepository.Query()
                .Where(x => userGroupIds.Contains(x.Id))
                .Include(x => x.User)
                .Include(x => x.GroupMessages).ToList();

            List<ChatViewModel> chatViewModels = new List<ChatViewModel>();

            List<GroupMessage> groupMessages = new();
            foreach(var userGroupWithMessage in userGroupsWithMessage)
            {
                groupMessages.AddRange(userGroupWithMessage.GroupMessages.Where(x => x.ShowToUsers != null && x.ShowToUsers.Contains(userId.ToString())));
            }

            groupMessages = groupMessages.OrderByDescending(x => x.CreatedDate).ToList();

            List<UserFriend> userFriends = await userFriendRepository.GetListAsync(x => x.UserId == userId);

            foreach(var userGroup in userGroups)
            {
                var message = groupMessages.FirstOrDefault();

                var friend = userFriends.FirstOrDefault(x => x.UserId == userId && x.PhoneNumber == userGroup.User.PhoneNumber);

                ChatViewModel chatViewModel = new()
                {
                    Id = userGroup.Group.Id,
                    Name = userGroup.Group.Name,
                    IsMyMessage = message != null && message.GroupUserId == userGroup.Id && userGroup.UserId == userId,
                    LastMessage = message != null ? message.Message : "",
                    UserName = message != null ? friend != null ? friend.Name : "~" + message.GroupUser.User.Name : "",
                    SendedDate = message != null ? message.CreatedDate : DateTime.Now,
                    IsGroup = true,
                    IsMyGroup = userGroup.Group.UserId == userId,
                    //HasBeenRead = message.HasBeenRead
                };

                chatViewModels.Add(chatViewModel);
            }

            return chatViewModels.OrderByDescending(x => x.SendedDate).ToList();
        }
    }
}
