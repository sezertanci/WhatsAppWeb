using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommand.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User existUser = await userRepository.GetSingleAsync(x => x.PhoneNumber == request.PhoneNumber);

            if(existUser != null)
                throw new DatabaseValidationException($"({request.PhoneNumber}) Bu telefon numarası daha önce kullanılmıştır!");

            User user = mapper.Map<User>(request);

            await userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
