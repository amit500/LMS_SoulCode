using LMS_SoulCode.Features.UserPermissions.Entities;

namespace LMS_SoulCode.Features.UserPermissions.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task<Permission> AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(Permission permission);
    }
}
