using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.Queries;
using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetGroupUsersQuery : IRequest<List<GetUsersViewModel>>
    {
        public Guid GroupId { get; set; }

        public GetGroupUsersQuery(Guid groupId)
        {
            GroupId = groupId;
        }

        public class GetGroupUsersQueryHandler : IRequestHandler<GetGroupUsersQuery, List<GetUsersViewModel>>
        {
            private readonly IGroupUserRepository groupUserRepository;
            private readonly IMapper mapper;

            public GetGroupUsersQueryHandler(IGroupUserRepository groupUserRepository, IMapper mapper)
            {
                this.groupUserRepository = groupUserRepository;
                this.mapper = mapper;
            }

            public async Task<List<GetUsersViewModel>> Handle(GetGroupUsersQuery request, CancellationToken cancellationToken)
            {
                List<GroupUser> groupUsers = await groupUserRepository.GetListAsync(x => x.GroupId == request.GroupId, includes: x => x.User);

                groupUsers = groupUsers.OrderByDescending(x => x.CreatedDate).ToList();

                return mapper.Map(groupUsers, new List<GetUsersViewModel>());
            }
        }
    }
}
