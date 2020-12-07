using AutoMapper;
using BB.Common.Dto.User;
using BB.DAL.Entities;

namespace BB.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
        
    }
}