using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.Queries;
using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetUsersQuery : IRequest<List<GetUsersViewModel>>
    {
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<GetUsersViewModel>>
        {
            private readonly IMapper mapper;
            private readonly IUserRepository userRepository;

            public GetUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
            {
                this.mapper = mapper;
                this.userRepository = userRepository;
            }

            public async Task<List<GetUsersViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                List<User> users = await userRepository.GetListAsync(x => !x.Deleted);

                return mapper.Map(users, new List<GetUsersViewModel>());
            }
        }
    }
}
