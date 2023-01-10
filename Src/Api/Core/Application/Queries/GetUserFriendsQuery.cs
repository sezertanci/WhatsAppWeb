using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.Queries;
using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetUserFriendsQuery : IRequest<List<GetUsersViewModel>>
    {
        public GetUserFriendsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }

        public class GetUserFriendsQueryHandler : IRequestHandler<GetUserFriendsQuery, List<GetUsersViewModel>>
        {
            private readonly IUserRepository userRepository;
            private readonly IUserFriendRepository userFriendRepository;
            private readonly IMapper mapper;

            public GetUserFriendsQueryHandler(IUserRepository userRepository, IUserFriendRepository userFriendRepository, IMapper mapper)
            {
                this.userRepository = userRepository;
                this.userFriendRepository = userFriendRepository;
                this.mapper = mapper;
            }

            public async Task<List<GetUsersViewModel>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
            {
                List<UserFriend> userFriends = await userFriendRepository.GetListAsync(x => x.UserId == request.UserId && !x.Deleted);

                if(userFriends == null) return null;

                List<User> users = await userRepository.GetListAsync(x => userFriends.Select(y => y.PhoneNumber).Contains(x.PhoneNumber) && !x.Deleted);

                return mapper.Map(users, new List<GetUsersViewModel>());
            }
        }
    }
}
