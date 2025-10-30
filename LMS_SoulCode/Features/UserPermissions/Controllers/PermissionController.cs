using LMS_SoulCode.Features.UserPermissions.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.UserPermissions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionService.GetAllAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            if (permission == null)
                return NotFound(new { message = "Permission not found" });

            return Ok(permission);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePermissionRequest request)
        {
            var permission = await _permissionService.CreateAsync(request.Name);
            return CreatedAtAction(nameof(GetById), new { id = permission.Id }, permission);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePermissionRequest request)
        {
            await _permissionService.UpdateAsync(id, request.Name);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _permissionService.DeleteAsync(id);
            return NoContent();
        }
    }

    public class CreatePermissionRequest
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UpdatePermissionRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
