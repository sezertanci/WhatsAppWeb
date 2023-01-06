using AutoMapper;
using Common.Models.Queries;
using Common.Models.RequestModels.GroupRequestModels;
using Common.Models.RequestModels.UserChatMessageRequestModels;
using Common.Models.RequestModels.UserFriendRequestModels;
using Common.Models.RequestModels.UserRequestModels;
using Common.Models.ViewModels;
using Domain.Models;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserCommand, User>().ReverseMap();
            CreateMap<LoginUserViewModel, User>().ReverseMap();
            CreateMap<GetUsersViewModel, User>().ReverseMap();

            CreateMap<CreateUserFriendCommand, UserFriend>().ReverseMap();
            CreateMap<UpdateUserFriendCommand, UserFriend>().ReverseMap();

            CreateMap<CreateUserChatMessageCommand, UserChatMessage>().ReverseMap();

            CreateMap<CreateGroupCommand, Group>().ReverseMap();

            CreateMap<UserChat, ChatViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name));
        }
    }
}
