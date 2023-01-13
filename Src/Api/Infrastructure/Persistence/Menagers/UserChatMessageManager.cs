using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Common.Models.ViewModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Menagers
{
    public class UserChatMessageManager : IUserChatMessageService
    {
        private readonly IUserChatMessageRepository userChatMessageRepository;
        private readonly IUserChatRepository userChatRepository;
        private readonly IChatRepository chatRepository;

        public UserChatMessageManager(IUserChatMessageRepository userChatMessageRepository, IUserChatRepository userChatRepository, IChatRepository chatRepository)
        {
            this.userChatMessageRepository = userChatMessageRepository;
            this.userChatRepository = userChatRepository;
            this.chatRepository = chatRepository;
        }

        public async Task<List<ChatMessageViewModel>> GetChatMessagesAsync(Guid userId, Guid friendUserId)
        {
            List<UserChat> userChats = await userChatRepository.GetListAsync(x => x.UserId == userId);

            List<UserChat> friendUserChats = await userChatRepository.GetListAsync(x => x.UserId == friendUserId);

            Guid chatId = userChats.Select(x => x.ChatId).ToList().Intersect(friendUserChats.Select(x => x.ChatId).ToList()).FirstOrDefault();

            Chat chat = await chatRepository.Query().Where(x => x.Id == chatId).Include(x => x.UserChats).ThenInclude(x => x.UserChatMessages).FirstOrDefaultAsync();

            List<ChatMessageViewModel> chatMessageViewModels = new();

            foreach(var userChat in chat.UserChats)
            {
                foreach(var userChatMessage in userChat.UserChatMessages.Where(x => !x.Deleted))
                {
                    bool isMyMessage = userChatMessage.UserChatId == userChat.Id && userChat.UserId == userId;

                    ChatMessageViewModel chatMessageViewModel = new()
                    {
                        IsMyMessage = isMyMessage,
                        Message = userChatMessage.Message,
                        SendedDate = userChatMessage.CreatedDate,
                        HasBeenRead = userChatMessage.HasBeenRead
                    };

                    chatMessageViewModels.Add(chatMessageViewModel);
                }
            }

            chatMessageViewModels = chatMessageViewModels.OrderBy(x => x.SendedDate).ToList();

            return chatMessageViewModels;
        }

        public async Task HasBeenReadChatMessagesAsync(Guid userId, Guid friendUserId)
        {
            List<UserChat> userChats = await userChatRepository.GetListAsync(x => x.UserId == userId);

            List<UserChat> friendUserChats = await userChatRepository.GetListAsync(x => x.UserId == friendUserId);

            Guid chatId = userChats.Select(x => x.ChatId).ToList().Intersect(friendUserChats.Select(x => x.ChatId).ToList()).FirstOrDefault();

            Chat chat = await chatRepository.Query().Where(x => x.Id == chatId).Include(x => x.UserChats).FirstOrDefaultAsync();

            Guid userChatId = chat.UserChats.FirstOrDefault(x => x.UserId == friendUserId).Id;

            var userChatMessage = await userChatMessageRepository.GetListAsync(x => x.UserChatId == userChatId && !x.HasBeenRead, false);

            userChatMessage.ForEach(x => x.HasBeenRead = true);

            await userChatMessageRepository.SaveChangesAsync();
        }

        public async Task<ChatMessageViewModel> SendMessageAsync(SendedMessageViewModel sendedMessageViewModel)
        {
            List<UserChat> senderUserChats = await userChatRepository.GetListAsync(x => x.UserId == sendedMessageViewModel.SenderUserId);

            List<UserChat> receiverUserChats = await userChatRepository.GetListAsync(x => x.UserId == sendedMessageViewModel.ReceiverUserId);

            Guid chatId = senderUserChats.Select(x => x.ChatId).ToList().Intersect(receiverUserChats.Select(x => x.ChatId).ToList()).FirstOrDefault();

            if(chatId == Guid.Empty)
            {
                Chat chat = new();
                await chatRepository.AddAsync(chat);

                UserChat senderUserChat = new()
                {
                    ChatId = chat.Id,
                    UserId = sendedMessageViewModel.SenderUserId
                };

                UserChat receiverUserChat = new()
                {
                    ChatId = chat.Id,
                    UserId = sendedMessageViewModel.ReceiverUserId
                };

                List<UserChat> userChats = new()
                {
                    senderUserChat,
                    receiverUserChat
                };

                await userChatRepository.AddRangeAsync(userChats);

                UserChatMessage userChatMessage = new()
                {
                    Message = sendedMessageViewModel.Message,
                    UserChatId = userChats.FirstOrDefault(x => x.UserId == sendedMessageViewModel.SenderUserId).Id
                };

                await userChatMessageRepository.AddAsync(userChatMessage);

                ChatMessageViewModel receivedMessageViewModel = new()
                {
                    Message = sendedMessageViewModel.Message,
                    SendedDate = userChatMessage.CreatedDate,
                    SenderUserId = senderUserChat.UserId,
                    HasBeenRead = false,
                    ChatId = sendedMessageViewModel.ReceiverUserId
                };

                return receivedMessageViewModel;
            }
            else
            {
                UserChatMessage userChatMessage = new()
                {
                    Message = sendedMessageViewModel.Message,
                    UserChatId = senderUserChats.FirstOrDefault(x => x.UserId == sendedMessageViewModel.SenderUserId && x.ChatId == chatId).Id
                };

                await userChatMessageRepository.AddAsync(userChatMessage);

                ChatMessageViewModel receivedMessageViewModel = new()
                {
                    Message = sendedMessageViewModel.Message,
                    SendedDate = userChatMessage.CreatedDate,
                    SenderUserId = sendedMessageViewModel.SenderUserId,
                    HasBeenRead = false,
                    ChatId = sendedMessageViewModel.ReceiverUserId
                };

                return receivedMessageViewModel;
            }
        }
    }
}
