using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.GroupRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.GroupCommand
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IGroupRepository groupRepository;
        private readonly IUserRepository userRepository;
        private readonly IGroupUserRepository groupUserRepository;

        public CreateGroupCommandHandler(IMapper mapper, IGroupRepository groupRepository, IUserRepository userRepository, IGroupUserRepository groupUserRepository)
        {
            this.mapper = mapper;
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.groupUserRepository = groupUserRepository;
        }

        public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            request.UserIds.Add(request.UserId); // Grubu oluşturan ile katılımcıları birleştiriyor

            if(request.UserIds.Count < 2)
                throw new DatabaseValidationException($"Katılımcı olmadan grup oluşturamazsınız!");

            User existUser = await userRepository.GetByIdAsync(request.UserId);
            if(existUser == null)
                throw new DatabaseValidationException($"({request.UserId}) Bu Id ile ilgili kullanıcı bulunamadı!");

            Group group = mapper.Map<Group>(request);

            await groupRepository.AddAsync(group);

            List<User> users = await userRepository.GetListAsync(x => request.UserIds.Contains(x.Id));//Groupta olacak tüm kullanıcıları varsa getiriyor

            List<GroupUser> groupUsers = new();

            foreach(var user in users)
            {
                GroupUser groupUser = new()
                {
                    UserId = user.Id,
                    GroupId = group.Id
                };

                groupUsers.Add(groupUser);
            }

            if(groupUsers.Count > 0)
                await groupUserRepository.AddRangeAsync(groupUsers);

            return group.Id;
        }
    }
}
