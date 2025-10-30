using Microsoft.AspNetCore.Mvc;
using LMS_SoulCode.Features.Auth.Models;
using LMS_SoulCode.Features.Auth.Services;
using LMS_SoulCode.Features.Auth.Validators;
using AuthModel = LMS_SoulCode.Features.Auth.Models.LoginRequest;
using RegisterModel = LMS_SoulCode.Features.Auth.Models.RegisterRequest;
using ForgotPasswordModel = LMS_SoulCode.Features.Auth.Models.ForgotPasswordRequest;
using ResetPasswordModel = LMS_SoulCode.Features.Auth.Models.ResetPasswordRequest;

namespace LMS_SoulCode.Features.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly LoginRequestValidator _loginValidator = new();
        private readonly RegisterRequestValidator _registerValidator = new();
        private readonly ForgotPasswordRequestValidator _forgotPasswordValidator = new();
        private readonly ResetPasswordRequestValidator _resetPasswordValidator = new();

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthModel request)
        {
            var validation = await _loginValidator.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            try
            {
                var response = await _auth.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel request)
        {
            var validation = await _registerValidator.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            try
            {
                var response = await _auth.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel request)
        {
            var validation = await _forgotPasswordValidator.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            try
            {
                var response = await _auth.ForgotPasswordAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Password reset failed: {ex.Message}");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel request)
        {
            var validation = await _resetPasswordValidator.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            try
            {
                var response = await _auth.ResetPasswordAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Password reset failed: {ex.Message}");
            }
        }
    }
}
