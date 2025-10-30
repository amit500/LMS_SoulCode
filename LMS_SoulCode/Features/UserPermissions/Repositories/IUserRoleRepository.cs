using LMS_SoulCode.Features.UserPermissions.Entities;

namespace LMS_SoulCode.Features.UserPermissions.Repositories
{
    public interface IUserRoleRepository
    {
        Task AssignRoleAsync(int userId, int roleId);
        Task RemoveRoleAsync(int userId, int roleId);
        Task<IEnumerable<Role>> GetUserRolesAsync(int userId);
    }
}
