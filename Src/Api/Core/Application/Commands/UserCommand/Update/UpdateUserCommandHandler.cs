using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommand.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User existUser = await userRepository.GetByIdAsync(request.Id);

            if(existUser == null)
                throw new DatabaseValidationException($"({request.Id}) Bu Id değerine sahip bir kayıt bulunamadı!");

            User existUserPhone = await userRepository.GetSingleAsync(x => x.Id != request.Id && x.PhoneNumber == request.PhoneNumber);

            if(existUserPhone != null)
                throw new DatabaseValidationException($"({request.PhoneNumber}) Bu telefon numarası daha önce kullanılmıştır. Başka bir numara kullanın.");

            mapper.Map(request, existUser);

            int result = await userRepository.UpdateAsync(existUser);

            return result > 0;
        }
    }
}
