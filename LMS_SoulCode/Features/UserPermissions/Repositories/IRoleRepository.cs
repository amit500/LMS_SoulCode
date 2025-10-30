using LMS_SoulCode.Features.UserPermissions.Entities;

namespace LMS_SoulCode.Features.UserPermissions.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<Role> AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(Role role);
    }
}
