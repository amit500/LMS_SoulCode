using LMS_SoulCode.Data;
using LMS_SoulCode.Features.UserPermissions.Entities;
using LMS_SoulCode.Features.UserPermissions.Repositories;
using System.Security;

namespace LMS_SoulCode.Features.UserPermissions.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<Role> CreateAsync(string roleName, List<int>? permissionIds);
        Task UpdateAsync(int id, string newName, List<int>? permissionIds);
        Task DeleteAsync(int id);
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

        public async Task<IEnumerable<Role>> GetAllAsync() => await _role.GetAllAsync();

        public async Task<Role?> GetByIdAsync(int id) => await _role.GetByIdAsync(id);


        public async Task<Role> CreateAsync(string roleName, List<int>? permissionIds)
        {
            var role = new Role
            {
                Name = roleName
            };

            if (permissionIds != null)
            {
                foreach (var pid in permissionIds)
                {
                    //role.RolePermissions.Add(new RolePermission { PermissionId = pid });
                }
            }

            return await _role.AddAsync(role);
        }

        public async Task UpdateAsync(int id, string newName, List<int>? permissionIds)
        {
            var role = await _role.GetByIdAsync(id);
            if (role == null)
                throw new Exception("Role not found");

            role.Name = newName;

            // Update permissions
            if (permissionIds != null)
            {
                role.RolePermissions.Clear();
                foreach (var pid in permissionIds)
                {
                    role.RolePermissions.Add(new RolePermission { PermissionId = pid });
                }
            }

            await _role.UpdateAsync(role);
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _role.GetByIdAsync(id);
            if (role == null)
                throw new Exception("Role not found");

            await _role.DeleteAsync(role);
        }
    }
}
