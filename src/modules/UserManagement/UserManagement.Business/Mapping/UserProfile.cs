using AutoMapper;
using UserManagement.Entity.Concrete;
using System;
using UserManagement.Entity.DTOs;

namespace UserManagement.Business.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //User -> UserDto Mapping
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture ?? string.Empty))
                .ReverseMap();

            //UserRegisterDto -> User Mapping
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // ID'yi otomatik olu?tur
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Kullan?c? olu?turulma tarihini set et
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // Güncellenme tarihini bo? b?rak
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Hash ?ifreleme s?ras?nda olu?turulacak
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(_ => false)) // Kullan?c? ba?ta do?rulanmam?? olsun
                .ForMember(dest => dest.TotalSwaps, opt => opt.MapFrom(_ => 0)) // Yeni kullan?c? için ba?lang?ç de?eri
                .ForMember(dest => dest.SuccessfulSwaps, opt => opt.MapFrom(_ => 0))
                .ReverseMap();

            //UserUpdateDto -> User Mapping
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // ID de?i?tirilemez
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // ?ifre de?i?tirilmez
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Güncelleme tarihi ekleniyor
                .ReverseMap();

            //UserChangePasswordDto -> User Mapping**
            CreateMap<UserChangePasswordDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // ID de?i?tirilemez
                .ForMember(dest => dest.FirstName, opt => opt.Ignore()) // Kullan?c? bilgileri de?i?tirilemez
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.Bio, opt => opt.Ignore())
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.TotalSwaps, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Güncelleme tarihi ekleniyor
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // ?ifre hashing s?ras?nda de?i?ecek
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
