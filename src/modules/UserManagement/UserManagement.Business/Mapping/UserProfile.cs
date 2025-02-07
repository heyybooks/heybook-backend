using AutoMapper;
using UserManagement.Business.DTOs;
using UserManagement.Entity.Concrete;

namespace UserManagement.Business.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // User -> UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture ?? string.Empty))
                .ReverseMap();

            // UserCreateDto -> User
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ReverseMap();

            // User -> UserUpdateDto
            CreateMap<User, UserUpdateDto>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
