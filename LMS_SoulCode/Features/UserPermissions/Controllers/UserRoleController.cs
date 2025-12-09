using LMS_SoulCode.Features.UserPermissions.Models;
using LMS_SoulCode.Features.UserPermissions.DTOs;
using LMS_SoulCode.Features.UserPermissions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LMS_SoulCode.Features.UserPermissions.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
            => _userRoleService = userRoleService;
        

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromQuery] int userId, [FromQuery] int roleId)
        {
                await _userRoleService.AssignRoleAsync(userId, roleId);
            var response = new RoleResponse(roleId, "UserRole assigned successfully!");

            return Ok(response);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveRole([FromQuery] int userId, [FromQuery] int roleId)
        {
            await _userRoleService.RemoveRoleAsync(userId, roleId);
            var response = new RoleResponse(roleId, "UserRole removed successfully!");

            return Ok(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var roles = await _userRoleService.GetUserRolesAsync(userId);
            return Ok(roles);
        }
    }
}
