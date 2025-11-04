using LMS_SoulCode.Features.UserPermissions.Services;
using LMS_SoulCode.Features.UserPermissions.Models;
using LMS_SoulCode.Features.UserPermissions.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RoleRequest= LMS_SoulCode.Features.UserPermissions.Models.RoleRequest;
using CreateRoleRequest = LMS_SoulCode.Features.UserPermissions.Models.CreateRoleRequest;
using Microsoft.AspNetCore.Authorization;
using LMS_SoulCode.Features.UserPermissions.Entities;

//using UpdateRoleRequest = LMS_SoulCode.Features.UserPermissions.Models.UpdateRoleRequest;
namespace LMS_SoulCode.Features.UserPermissions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
            => _roleService = roleService;

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
        {
            var validator = new RoleValidators.CreateRoleRequestValidator();
            ValidationResult result = await validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var role = await _roleService.CreateAsync(request);
            var response = new RoleResponse(role.Id, "Role created successfully!");

            return CreatedAtAction(nameof(GetById), new { id = role.Id }, response);

        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateRoleRequest request)
        {
            var validator = new RoleValidators.CreateRoleRequestValidator();
            ValidationResult result = await validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            await _roleService.UpdateAsync(id, request);
            var response = new RoleResponse(id, "Role Updated successfully!");

            return Ok(response);
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
            var response = new RoleResponse(id, "Role Deleted successfully!");
            return Ok(response);
        }
    }

    
}
