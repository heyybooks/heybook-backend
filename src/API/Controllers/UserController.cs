using Microsoft.AspNetCore.Mvc;
using UserManagement.Business.Abstract;
using Core.Utilities.Results.Abstract;
using UserManagement.Entity.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
                return BadRequest("User register data is required.");

            var result = await _userService.Register(userRegisterDto, userRegisterDto.Password);
            return HandleResult(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
                return BadRequest("User login data is required.");

            var result = await _userService.Login(userLoginDto);
            return HandleResult(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return HandleDataResult(result);
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null)
                return BadRequest("User update data is required.");

            var result = await _userService.UpdateUser(userId, userUpdateDto);
            return HandleResult(result);
        }

        [HttpPut("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] UserChangePasswordDto passwordDto)
        {
            if (passwordDto == null)
                return BadRequest("Password change data is required.");

            var result = await _userService.ChangePassword(userId, passwordDto);
            return HandleResult(result);
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _userService.GetUserById(userId);
            return HandleDataResult(result);
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUser(userId);
            return HandleResult(result);
        }


        private IActionResult HandleResult(Core.Utilities.Results.Abstract.IResult result)
        {
            if (result.IsSuccess)
                return Ok(new { Message = result.Message });
            return BadRequest(new { Message = result.Message });
        }

        private IActionResult HandleDataResult<T>(IDataResult<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Data);
            return NotFound(new { Message = result.Message });
        }
    }
}
