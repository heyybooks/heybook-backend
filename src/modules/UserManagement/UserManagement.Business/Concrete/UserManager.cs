using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using UserManagement.Business.Abstract;

using UserManagement.DataAccess.Abstract;
using UserManagement.Entity.Concrete;
using System.Security.Cryptography;
using Microsoft.Identity.Client;
using UserManagement.Business.Utilities;
using UserManagement.Entity.DTOs;
using UserManagement.Business.Constants;
using Core.Aspect.Autofac.Validation;
using UserManagement.Business.ValidationRules.FluentValidation;

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

        [ValidationAspect(typeof(UserRegisterValidator))]
        public async Task<IResult> Register(UserRegisterDto userRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userRegisterDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.CreatedAt = DateTime.UtcNow;

            await _userDal.AddAsync(user);
            return new SuccessResult("User registered successfully");
        }
        [ValidationAspect(typeof(UserLoginValidator))]
        public async Task<IDataResult<string>> Login(UserLoginDto userLoginDto)
        {
            var user = await _userDal.GetAsync(u => u.Email == userLoginDto.Email);
            if (user == null)
            {
                return new ErrorDataResult<string>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<string>(Messages.PasswordInvalid);
            }

            return new SuccessDataResult<string>(Messages.SuccessfullLogin);
        }
        public async Task<IDataResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userDal.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return new SuccessDataResult<List<UserDto>>(userDtos, Messages.UserListed);
        }

        public async Task<IDataResult<UserDto>> GetUserById(int userId)
        {
            var user = await _userDal.GetAsync(u => u.UserId == userId);
            if (user == null)
            {
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto);
        }

        public async Task<IResult> UpdateUser(int userId, UserUpdateDto userUpdateDto)
        {
            var user = await _userDal.GetAsync(u => u.UserId == userId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            _mapper.Map(userUpdateDto, user);
            user.UpdatedAt = DateTime.UtcNow;

            await _userDal.UpdateAsync(user);
            return new SuccessResult(Messages.UserUpdated);
        }


        public async Task<IResult> DeleteUser(int userId)
        {
            var user = await _userDal.GetAsync(u => u.UserId == userId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            await _userDal.DeleteAsync(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public async Task<IResult> ChangePassword(int userId, UserChangePasswordDto passwordDto)
        {
            var user = await _userDal.GetAsync(u => u.UserId == userId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            // Eski ?ifre do?rulamas?
            using (var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordDto.CurrentPassword));
                if (!computedHash.SequenceEqual(user.PasswordHash))
                {
                    return new ErrorResult(Messages.PasswordIncorrect);
                }
            }

            if (passwordDto.NewPassword != passwordDto.ConfirmNewPassword)
            {
                return new ErrorResult(Messages.PasswordsDoNotMatch);
            }

            // Yeni ?ifre olu?tur
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordDto.NewPassword));
            }

            user.UpdatedAt = DateTime.UtcNow;
            await _userDal.UpdateAsync(user);

            return new SuccessResult(Messages.PasswordChangedSuccessfully);
        }

    }
}
