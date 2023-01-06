using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserChatMessageRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.UserChatMessageCommand.Create
{
    public class CreateUserChatMessageCommandHandler : IRequestHandler<CreateUserChatMessageCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserChatMessageRepository userChatMessageRepository;
        private readonly IUserChatRepository userChatRepository;

        public CreateUserChatMessageCommandHandler(IMapper mapper, IUserChatMessageRepository userChatMessageRepository, IUserChatRepository userChatRepository)
        {
            this.mapper = mapper;
            this.userChatMessageRepository = userChatMessageRepository;
            this.userChatRepository = userChatRepository;
        }

        public async Task<Guid> Handle(CreateUserChatMessageCommand request, CancellationToken cancellationToken)
        {
            UserChat userChat = await userChatRepository.GetByIdAsync(request.UserChatId);

            if(userChat == null)
                throw new DatabaseValidationException($"({request.UserChatId}) Bu Id'ye ait bir chat odası bulunamadı!");

            UserChatMessage userChatMessage = mapper.Map<UserChatMessage>(request);

            await userChatMessageRepository.AddAsync(userChatMessage);

            return userChatMessage.Id;
        }
    }
}
