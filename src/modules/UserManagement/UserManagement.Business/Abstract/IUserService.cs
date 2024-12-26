using Core.Utilities.Results.Abstract;
using UserManagement.Business.DTOs;

namespace UserManagement.Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<UserDto>>> GetAllUsers();
        Task<IDataResult<UserDto>> GetUserById(int userId);
        Task<IDataResult<UserDto>> GetUserByEmail(string email);
        Task<IResult> CreateUser(UserCreateDto userCreateDto);
        Task<IResult> UpdateUser(UserUpdateDto userUpdateDto);
        Task<IResult> DeleteUser(int userId);
        Task<IDataResult<UserDto>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
