using LMS_SoulCode.Features.UserPermissions.Entities;
using LMS_SoulCode.Features.UserPermissions.Repositories;

namespace LMS_SoulCode.Features.UserPermissions.Services
{
    public interface IUserRoleService
    {
        Task AssignRoleAsync(int userId, int roleId);
        Task RemoveRoleAsync(int userId, int roleId);
        Task<IEnumerable<Role>> GetUserRolesAsync(int userId);
    }
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            await _userRoleRepository.AssignRoleAsync(userId, roleId);
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            await _userRoleRepository.RemoveRoleAsync(userId, roleId);
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(int userId)
        {
            return await _userRoleRepository.GetUserRolesAsync(userId);
        }
    }
}
