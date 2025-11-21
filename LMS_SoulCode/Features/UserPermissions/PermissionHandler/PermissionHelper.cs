using LMS_SoulCode.Features.Auth.Repositories;
using LMS_SoulCode.Features.UserPermissions.Repositories;
using System.Security.Claims;

namespace LMS_SoulCode.Features.UserPermissions.PermissionHandler
{
    public interface IPermissionHelper
    {
        Task<bool> CheckPermission(string configKey);
    }

    public class PermissionHelper : IPermissionHelper
    {
        private readonly IConfiguration _config;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IHttpContextAccessor _httpContext;

        public PermissionHelper(IConfiguration config, IUserRoleRepository userRoleRepo, IHttpContextAccessor httpContext)
        {
            _config = config;
            _userRoleRepo = userRoleRepo;
            _httpContext = httpContext;
        }
        public async Task<bool> CheckPermission(string configKey)
        {
            string keyFromConfig = _config[$"PermissionKeys:{configKey}"];

            var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return false;
            int userId = int.Parse(userIdClaim.Value);

            return await _userRoleRepo.UserHasPermission(userId, keyFromConfig);
        }

    }

}
