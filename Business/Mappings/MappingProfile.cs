using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;

namespace Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailDTO>();
            CreateMap<UserDetailDTO, User>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, UserAddDTO>();
            CreateMap<UserAddDTO, User>();

            CreateMap<User, UserUpdateDTO>();
            CreateMap<UserUpdateDTO, User>();
        }
    }
}