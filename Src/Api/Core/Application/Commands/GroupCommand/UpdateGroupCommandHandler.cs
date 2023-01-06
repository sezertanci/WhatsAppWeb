using Application.Interfaces.Repositories;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.GroupRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Commands.GroupCommand
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, bool>
    {
        private readonly IGroupRepository groupRepository;

        public UpdateGroupCommandHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<bool> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            Group group = await groupRepository.GetByIdAsync(request.Id);

            if(group == null)
                throw new DatabaseValidationException($"({request.Id}) Bu Id ile ilgili grup bulunamadı!");

            if(group.UserId != request.UserId)
                throw new DatabaseValidationException($"({request.UserId}) Bu Id'ye sahip kullanıcı bu işlemi gerçekleştiremez!");

            group.Name = request.Name;

            int result = await groupRepository.UpdateAsync(group);

            return result > 0;
        }
    }
}
