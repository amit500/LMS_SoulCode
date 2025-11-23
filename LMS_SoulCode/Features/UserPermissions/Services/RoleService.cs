using LMS_SoulCode.Data;
using LMS_SoulCode.Features.UserPermissions.Models;
using LMS_SoulCode.Features.UserPermissions.Repositories;
using CreateRoleRequest = LMS_SoulCode.Features.UserPermissions.DTOs.CreateRoleRequest;
using RoleRequest = LMS_SoulCode.Features.UserPermissions.DTOs.RoleRequest;
using RoleResponse = LMS_SoulCode.Features.UserPermissions.DTOs.RoleResponse;
using System.Security;
using Microsoft.AspNetCore.Mvc;
using LMS_SoulCode.Features.UserPermissions.Models;
using LMS_SoulCode.Features.Auth.Models;

namespace LMS_SoulCode.Features.UserPermissions.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<Role> CreateAsync(CreateRoleRequest request);
        Task<Role> UpdateAsync(int id, CreateRoleRequest request);
        Task<Role>  DeleteAsync(int id);
        Task<IEnumerable<object>> GetAllRolePermissionAsync();
        
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _role;
        private readonly LmsDbContext _context;

        public RoleService(IRoleRepository roleRepo, LmsDbContext context)
        {
            _role = roleRepo;
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllAsync() => await _role.GetAllAsync();
        public async Task<IEnumerable<object>> GetAllRolePermissionAsync() => await _role.GetAllRolePermissionAsync();

        public async Task<Role?> GetByIdAsync(int id) => await _role.GetByIdAsync(id);


        public async Task<Role> CreateAsync([FromBody] CreateRoleRequest request)
        {
            var role = new Role
            {
                Name = request.RoleName
            };

            if (request.PermissionIds != null)
            {
                foreach (var pid in request.PermissionIds)
                {
                    role.RolePermissions.Add(new RolePermission { PermissionId = pid });
                }
            }

             await _role.AddAsync(role);
            return role;

        }


        public async Task<Role> UpdateAsync(int id, [FromBody] CreateRoleRequest request)
        {
            var role = await _role.GetByIdAsync(id);
            if (role == null)
                throw new Exception("Role not found");

            role.Name = request.RoleName;

            // Update permissions
            var existingPermissions = _context.RolePermissions.Where(rp => rp.RoleId == role.Id);
            _context.RolePermissions.RemoveRange(existingPermissions);

            if (request.PermissionIds != null)
            {
                role.RolePermissions.Clear();
                foreach (var pid in request.PermissionIds)
                {
                    role.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = pid });    
                }
            }

            await _role.UpdateAsync(role);
            return role;

        }

        public async Task<Role> DeleteAsync(int id)
        {
            var role = await _role.GetByIdAsync(id);
            if (role == null)
                throw new Exception("Role not found");

            await _role.DeleteAsync(role);

            return role;

        }
    }
}
