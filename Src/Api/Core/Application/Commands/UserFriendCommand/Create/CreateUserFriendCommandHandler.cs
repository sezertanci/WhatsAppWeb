using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserFriendRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.UserFriendCommand.Create
{
    public class CreateUserFriendCommandHandler : IRequestHandler<CreateUserFriendCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserFriendRepository userFriendRepository;

        public CreateUserFriendCommandHandler(IMapper mapper, IUserFriendRepository userFriendRepository)
        {
            this.mapper = mapper;
            this.userFriendRepository = userFriendRepository;
        }

        public async Task<Guid> Handle(CreateUserFriendCommand request, CancellationToken cancellationToken)
        {
            UserFriend existUserFriend = await userFriendRepository.GetSingleAsync(x => x.UserId == request.UserId && x.PhoneNumber == request.PhoneNumber);

            if(existUserFriend != null)
                throw new DatabaseValidationException($"Bu telefon numarası ile kayıtlı bir arkadaşınız bulunmaktadır.({existUserFriend.Name} - {existUserFriend.PhoneNumber})");

            UserFriend userFriend = mapper.Map<UserFriend>(request);

            await userFriendRepository.AddAsync(userFriend);

            return userFriend.Id;
        }
    }
}
