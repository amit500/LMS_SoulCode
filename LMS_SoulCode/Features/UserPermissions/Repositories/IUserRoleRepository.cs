using LMS_SoulCode.Features.UserPermissions.Models;

namespace LMS_SoulCode.Features.UserPermissions.Repositories
{
    public interface IUserRoleRepository
    {
        Task AssignRoleAsync(int userId, int roleId);
        Task RemoveRoleAsync(int userId, int roleId);
        Task<IEnumerable<object>> GetUserRolesAsync(int userId);
        Task<bool> UserHasPermission(int userId, string key);

    }
}
