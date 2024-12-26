using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using UserManagement.Business.Abstract;
using UserManagement.Business.DTOs;
using UserManagement.DataAccess.Abstract;
using UserManagement.Entity.Concrete;
using System.Security.Cryptography;
using Microsoft.Identity.Client;
using UserManagement.Business.Utilities;

namespace UserManagement.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userDal.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return new SuccessDataResult<List<UserDto>>(userDtos);
        }

        public async Task<IDataResult<UserDto>> GetUserById(int userId)
        {
            var user = await _userDal.GetByIdAsync(userId);
            if (user == null)
                return new ErrorDataResult<UserDto>("Kullanıcı bulunamadı");

            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto);
        }

        public async Task<IDataResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userDal.GetByEmail(email);
            if (user == null)
                return new ErrorDataResult<UserDto>("Kullanıcı bulunamadı");

            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto);
        }

        public async Task<IResult> CreateUser(UserCreateDto userCreateDto)
        {
            var userExists = await UserExists(userCreateDto.Email);
            if (userExists)
                return new ErrorResult("Bu email zaten kayıtlı");

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userCreateDto.Password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _userDal.AddAsync(user);
            return new SuccessResult("Kullanıcı başarıyla oluşturuldu");
        }

        public async Task<IResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var userToUpdate = await _userDal.GetByIdAsync(userUpdateDto.UserId);
            
            if (userToUpdate == null)
                return new ErrorResult("Kullanıcı bulunamadı");

            // AutoMapper ile güncelleme işlemi
            _mapper.Map(userUpdateDto, userToUpdate);
            userToUpdate.UpdatedAt = DateTime.UtcNow; // Güncelleme tarihini ayarla

            bool updateResult = await _userDal.UpdateAsync(userToUpdate);

            return updateResult 
                ? new SuccessResult("Kullanıcı bilgileri güncellendi") 
                : new ErrorResult("Güncelleme işlemi başarısız");
        }

        public async Task<IResult> DeleteUser(int userId)
        {
            var userToDelete = await _userDal.GetAsync(u => u.UserId == userId);
            if (userToDelete == null)
                return new ErrorResult("Kullanıcı bulunamadı");

            await _userDal.DeleteAsync(userToDelete);
            return new SuccessResult("Kullanıcı silindi");
        }

        public async Task<IDataResult<UserDto>> Login(string email, string password)
        {
            var user = await _userDal.GetByEmail(email);
            if (user == null)
                return new ErrorDataResult<UserDto>("Kullanıcı bulunamadı");

            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new ErrorDataResult<UserDto>("Şifre yanlış");

            user.LastLoginDate = DateTime.UtcNow;
            await _userDal.UpdateAsync(user);

            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto);
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _userDal.GetByEmail(email);
            return user != null;
        }
    }
}
