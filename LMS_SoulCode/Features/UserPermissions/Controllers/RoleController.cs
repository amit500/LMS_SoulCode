using LMS_SoulCode.Features.UserPermissions.Services;
using LMS_SoulCode.Features.UserPermissions.Models;
using LMS_SoulCode.Features.UserPermissions.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.UserPermissions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
        {
            var validator = new RoleValidators.CreateRoleRequestValidator();
            ValidationResult result = await validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var role = await _roleService.CreateAsync(request.Name, request.PermissionIds);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleRequest request)
        {
            var validator = new RoleValidators.UpdateRoleRequestValidator();
            ValidationResult result = await validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            await _roleService.UpdateAsync(id, request.Name, request.PermissionIds);
            return NoContent();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            return Ok(role);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteAsync(id);
            return NoContent();
        }
    }

    public class CreateRoleRequest
    {
        public string Name { get; set; } = string.Empty;
        public List<int>? PermissionIds { get; set; }
    }

    public class UpdateRoleRequest
    {
        public string Name { get; set; } = string.Empty;
        public List<int>? PermissionIds { get; set; }
    }
}
