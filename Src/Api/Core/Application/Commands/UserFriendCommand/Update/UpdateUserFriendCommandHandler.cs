using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserFriendRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.UserFriendCommand.Update
{
    public class UpdateUserFriendCommandHandler : IRequestHandler<UpdateUserFriendCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IUserFriendRepository userFriendRepository;

        public UpdateUserFriendCommandHandler(IMapper mapper, IUserFriendRepository userFriendRepository)
        {
            this.mapper = mapper;
            this.userFriendRepository = userFriendRepository;
        }

        public async Task<bool> Handle(UpdateUserFriendCommand request, CancellationToken cancellationToken)
        {
            UserFriend existUserFriend = await userFriendRepository.GetByIdAsync(request.Id);

            if(existUserFriend == null)
                throw new DatabaseValidationException($"({request.Id}) Bu Id değerine sahip bir kayıt bulunamadı!");

            UserFriend existUserFriendPhone = await userFriendRepository.GetSingleAsync(x => x.Id != request.Id && x.UserId == request.UserId && x.PhoneNumber == request.PhoneNumber);

            if(existUserFriendPhone != null)
                throw new DatabaseValidationException($"Bu telefon numarası ile kayıtlı bir arkadaşınız bulunmaktadır.({existUserFriend.Name} - {existUserFriend.PhoneNumber})");

            mapper.Map(request, existUserFriend);

            int result = await userFriendRepository.UpdateAsync(existUserFriend);

            return result > 0;
        }
    }
}
