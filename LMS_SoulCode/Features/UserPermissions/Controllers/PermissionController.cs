using LMS_SoulCode.Features.UserPermissions.Entities;
using LMS_SoulCode.Features.UserPermissions.Models;
using CreatePermissionRequest = LMS_SoulCode.Features.UserPermissions.Models.CreatePermissionRequest;
using LMS_SoulCode.Features.UserPermissions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace LMS_SoulCode.Features.UserPermissions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService) 
            => _permissionService = permissionService;
        
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
            var permission = await _permissionService.CreateAsync(request.PermissionName);
            var response = new RoleResponse(permission.Id, "Permission created successfully!");

            return CreatedAtAction(nameof(GetById), new { id = permission.Id }, response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePermissionRequest request)
        {
            await _permissionService.UpdateAsync(id, request.PermissionName);
            var response = new RoleResponse(id, "Permission updated successfully!");

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _permissionService.DeleteAsync(id);
            var response = new RoleResponse(id, "Permission deleted successfully!");

            return Ok(response);
        }
    }    
}
