using LMS_SoulCode.Features.UserPermissions.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.UserPermissions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromQuery] int userId, [FromQuery] int roleId)
        {
            await _userRoleService.AssignRoleAsync(userId, roleId);
            return Ok(new { Message = "Role assigned successfully!" });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveRole([FromQuery] int userId, [FromQuery] int roleId)
        {
            await _userRoleService.RemoveRoleAsync(userId, roleId);
            return Ok(new { Message = "Role removed successfully!" });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var roles = await _userRoleService.GetUserRolesAsync(userId);
            return Ok(roles);
        }
    }
}
