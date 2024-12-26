using AutoMapper;
using UserManagement.Business.DTOs;
using UserManagement.Entity.Concrete;

namespace UserManagement.Business.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture ?? string.Empty))
                .ReverseMap();

            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // ID'yi ignore et
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Oluşturulma tarihini otomatik set et
                .ReverseMap();

            CreateMap<User, UserUpdateDto>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // Güncelleme DTO'sunda UpdatedAt'ı ignore et
                .ReverseMap();
        }
    }
}
