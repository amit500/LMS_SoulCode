using LMS_SoulCode.Features.Auth.DTOs;
using LMS_SoulCode.Features.Auth.Repositories;
using LMS_SoulCode.Features.Auth.Services;
using LMS_SoulCode.Features.Auth.Models;
using AuthModel = LMS_SoulCode.Features.Auth.DTOs.LoginRequest;
using RegisterModel = LMS_SoulCode.Features.Auth.DTOs.RegisterRequest;
using ForgotPasswordModel = LMS_SoulCode.Features.Auth.DTOs.ForgotPasswordRequest;
using ResetPasswordModel = LMS_SoulCode.Features.Auth.DTOs.ResetPasswordRequest;
using LMS_SoulCode.Features.Course.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LMS_SoulCode.Features.Auth.Services
{
    public interface IUserService
    {
       
         Task<IEnumerable<UserDto>> GetAllUserAsync();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
      
        public UserService(IUserRepository users)
        {
            _users = users;
        }


        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        =>  await _users.GetAllUserAsync();
        

    }
}
