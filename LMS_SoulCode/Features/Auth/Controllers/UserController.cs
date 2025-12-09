using Microsoft.AspNetCore.Mvc;
using LMS_SoulCode.Features.Auth.Models;
using LMS_SoulCode.Features.Auth.Services;
using LMS_SoulCode.Features.Auth.Validators;
using AuthModel = LMS_SoulCode.Features.Auth.DTOs.LoginRequest;
using RegisterModel = LMS_SoulCode.Features.Auth.DTOs.RegisterRequest;
using ForgotPasswordModel = LMS_SoulCode.Features.Auth.DTOs.ForgotPasswordRequest;
using ResetPasswordModel = LMS_SoulCode.Features.Auth.DTOs.ResetPasswordRequest;
using Microsoft.AspNetCore.Authorization;

namespace LMS_SoulCode.Features.Auth.Controllers
{
    [Authorize]

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly LoginRequestValidator _loginValidator = new();
        private readonly RegisterRequestValidator _registerValidator = new();
        private readonly ForgotPasswordRequestValidator _forgotPasswordValidator = new();
        private readonly ResetPasswordRequestValidator _resetPasswordValidator = new();

        public UserController(IUserService user)
        {
            _user = user;
        }


        [HttpGet("UserList")]
        //[CheckPermission("CourseList")]

        public async Task<IActionResult> GetUserList()
        {
            var courses = await _user.GetAllUserAsync();
            return Ok(courses);
        }
    }
}
