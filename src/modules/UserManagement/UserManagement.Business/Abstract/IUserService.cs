using Core.Utilities.Results.Abstract;
using UserManagement.Entity.DTOs;

namespace UserManagement.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> Register(UserRegisterDto userRegisterDto, string password);
        Task<IDataResult<string>> Login(UserLoginDto userLoginDto);
        Task<IDataResult<List<UserDto>>> GetAllUsers();
        Task<IDataResult<UserDto>> GetUserById(int userId);
        Task<IResult> UpdateUser(int userId, UserUpdateDto userUpdateDto);
        Task<IResult> DeleteUser(int userId);
        Task<IResult> ChangePassword(int userId, UserChangePasswordDto passwordDto);
    }
}
