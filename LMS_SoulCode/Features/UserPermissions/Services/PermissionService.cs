using LMS_SoulCode.Features.UserPermissions.Entities;
using LMS_SoulCode.Features.UserPermissions.Repositories;

namespace LMS_SoulCode.Features.UserPermissions.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task<Permission> CreateAsync(string name);
        Task UpdateAsync(int id, string newName);
        Task DeleteAsync(int id);
    }
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permission;

        public PermissionService(IPermissionRepository permissionRepo)
        {
            _permission = permissionRepo;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
            => await _permission.GetAllAsync();

        public async Task<Permission?> GetByIdAsync(int id)
            => await _permission.GetByIdAsync(id);

        public async Task<Permission> CreateAsync(string name)
        {
            var permission = new Permission { Name = name };
            return await _permission.AddAsync(permission);
        }

        public async Task UpdateAsync(int id, string newName)
        {
            var permission = await _permission.GetByIdAsync(id);
            if (permission == null)
                throw new Exception("Permission not found");

            permission.Name = newName;
            await _permission.UpdateAsync(permission);
        }

        public async Task DeleteAsync(int id)
        {
            var permission = await _permission.GetByIdAsync(id);
            if (permission == null)
                throw new Exception("Permission not found");

            await _permission.DeleteAsync(permission);
        }
    }
}
