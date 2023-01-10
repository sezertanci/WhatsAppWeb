using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Common.Models.ViewModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Menagers
{
    public class UserChatManager : IUserChatService
    {
        private readonly IUserChatRepository userChatRepository;
        private readonly IUserChatMessageService userChatMessageService;
        private readonly IUserFriendRepository userFriendRepository;

        public UserChatManager(IUserChatRepository userChatRepository, IUserChatMessageService userChatMessageService, IUserFriendRepository userFriendRepository)
        {
            this.userChatRepository = userChatRepository;
            this.userChatMessageService = userChatMessageService;
            this.userFriendRepository = userFriendRepository;
        }

        public async Task<List<ChatViewModel>> UserChats(Guid userId)
        {
            List<UserChat> userChats = await userChatRepository.GetListAsync(x => x.UserId == userId);
            List<UserChat> friendUserChats = await userChatRepository.GetListAsync(x => userChats.Select(y => y.ChatId).Contains(x.ChatId) && x.UserId != userId, includes: y => y.User);

            List<Guid> userChatIds = new();
            userChatIds.AddRange(userChats.Select(x => x.Id));
            userChatIds.AddRange(friendUserChats.Select(x => x.Id));

            //List<UserChat> userChatsWithMessage = userChatRepository.Query().Where(x => userChatIds.Contains(x.Id)).Include(x => x.UserChatMessages).ToList();

            //List<UserChatMessage> userChatMessages = new();
            //foreach(var userChatWithMessage in userChatsWithMessage)
            //{
            //    userChatMessages.AddRange(userChatWithMessage.UserChatMessages);
            //}

            //userChatMessages = userChatMessages.OrderByDescending(x => x.CreatedDate).ToList();

            List<ChatViewModel> chatViewModels = new List<ChatViewModel>();

            List<UserFriend> userFriends = await userFriendRepository.GetListAsync(x => x.UserId == userId);

            foreach(var userChat in friendUserChats)
            {
                var messages = await userChatMessageService.GetChatMessagesAsync(userId, userChat.UserId);
                var message = messages.LastOrDefault();

                if(message == null) continue;

                var friend = userFriends.FirstOrDefault(x => x.UserId == userId && x.PhoneNumber == userChat.User.PhoneNumber);

                ChatViewModel chatViewModel = new()
                {
                    Id = userChat.UserId,
                    IsMyMessage = message.IsMyMessage,
                    LastMessage = message.Message,
                    Name = friend != null ? friend.Name : "~" + userChat.User.Name,
                    SendedDate = message.SendedDate,
                    HasBeenRead = message.HasBeenRead
                };

                chatViewModels.Add(chatViewModel);
            }

            return chatViewModels.OrderByDescending(x => x.SendedDate).ToList();
        }
    }
}
