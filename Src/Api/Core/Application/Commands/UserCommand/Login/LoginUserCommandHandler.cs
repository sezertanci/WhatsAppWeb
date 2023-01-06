using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserRequestModels;
using Common.Models.ViewModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommand.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public LoginUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetSingleAsync(x => x.PhoneNumber == request.PhoneNumber && x.Password == request.Password);

            if(user == null)
                throw new DatabaseValidationException("Girilen bilgiler hatalı!");

            return mapper.Map(user, new LoginUserViewModel());
        }
    }
}
