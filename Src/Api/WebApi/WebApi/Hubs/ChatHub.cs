using Application.Interfaces.Services;
using Common.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs.Interfaces;
using WebApi.Models;
using WebApi.SignalRData;

namespace WebApi.Hubs
{
    public class ChatHub : Hub<IChat>
    {
        private readonly IUserFriendService userFriendService;
        private readonly IUserChatMessageService userChatMessageService;
        private readonly IUserChatService userChatService;
        private readonly IGroupUserService groupUserService;
        private readonly IGroupMessageService groupMessageService;

        static List<string> clients = new();

        public ChatHub(IUserFriendService userFriendService, IUserChatMessageService userChatMessageService, IUserChatService userChatService, IGroupUserService groupUserService, IGroupMessageService groupMessageService)
        {
            this.userFriendService = userFriendService;
            this.userChatMessageService = userChatMessageService;
            this.userChatService = userChatService;
            this.groupUserService = groupUserService;
            this.groupMessageService = groupMessageService;
        }

        public void AddClient(Guid userId)
        {
            SignalRChatUser exist = ChatUserSource.SignalRChatUsers.FirstOrDefault(x => x.UserId == userId);

            if(exist == null)
            {
                SignalRChatUser signalRChatUser = new()
                {
                    UserId = userId,
                    ConnectionId = Context.ConnectionId
                };

                ChatUserSource.SignalRChatUsers.Add(signalRChatUser);
            }
        }

        public async Task GetUserFriendsAsync(Guid userId)
        {
            List<UserFriendViewModel> userFriendViewModels = await userFriendService.GetUserFriends(userId);

            await Clients.Caller.UserFriends(userFriendViewModels);
        }

        public async Task GetUserChatsAsync(Guid userId)
        {
            List<ChatViewModel> userChatViewModels = await userChatService.UserChats(userId);
            List<ChatViewModel> userGroupChatViewModels = await groupUserService.UserGroups(userId);

            List<ChatViewModel> result = new();
            result.AddRange(userChatViewModels);
            result.AddRange(userGroupChatViewModels);

            result = result.OrderByDescending(x => x.SendedDate).ToList();

            await Clients.Caller.UserChats(result);
        }

        public async Task GetChatMessagesAsync(Guid userId, Guid friendUserId)
        {
            List<ChatMessageViewModel> chatMessageViewModels = await userChatMessageService.GetChatMessagesAsync(userId, friendUserId);

            await Clients.Caller.ChatMessages(chatMessageViewModels);
        }

        public async Task GetGroupChatMessagesAsync(Guid userId, Guid groupId)
        {
            List<ChatMessageViewModel> chatMessageViewModels = await groupMessageService.GetChatMessagesAsync(userId, groupId);

            await Clients.Caller.ChatMessages(chatMessageViewModels);
        }

        public async Task SendMessageAsync(SendedMessageViewModel sendedMessageViewModel)
        {
            ChatMessageViewModel result = await userChatMessageService.SendMessageAsync(sendedMessageViewModel);

            List<SignalRChatUser> users = ChatUserSource.SignalRChatUsers.Where(x => x.UserId == sendedMessageViewModel.ReceiverUserId || x.UserId == sendedMessageViewModel.SenderUserId).ToList();

            List<string> connectionIds = users.Select(x => x.ConnectionId).ToList();

            await Clients.Clients(connectionIds).ReceivedMessage(result);
        }

        public async Task SendGroupMessageAsync(SendedGroupMessageViewModel sendedGroupMessageViewModel)
        {
            GroupMessageViewModel result = await groupMessageService.SendMessageAsync(sendedGroupMessageViewModel);

            List<SignalRChatUser> users = ChatUserSource.SignalRChatUsers.Where(x => result.UserIds.Contains(x.UserId)).ToList();

            List<string> connectionIds = users.Select(x => x.ConnectionId).ToList();

            await Clients.Clients(connectionIds).ReceivedMessage(result.ChatMessageViewModel);
        }

        public async Task HasBeenReadChatMessagesAsync(Guid userId, Guid friendUserId)
        {
            await userChatMessageService.HasBeenReadChatMessagesAsync(userId, friendUserId);

            SignalRChatUser user = ChatUserSource.SignalRChatUsers.FirstOrDefault(x => x.UserId == friendUserId);

            if(user != null)
            {
                await Clients.Clients(user.ConnectionId).Refresh();
            }
        }

        public override Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            clients.Remove(Context.ConnectionId);

            SignalRChatUser signalRChatUser = ChatUserSource.SignalRChatUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            ChatUserSource.SignalRChatUsers.Remove(signalRChatUser);
            return Task.CompletedTask;
        }
    }
}
