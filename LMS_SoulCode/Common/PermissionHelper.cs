using LMS_SoulCode.Features.Auth.Repositories;
using System.Security.Claims;

namespace LMS_SoulCode.Common
{
    public interface IPermissionHelper
    {
        Task<bool> CheckPermission(string configKey);
    }

    public class PermissionHelper : IPermissionHelper
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;
        private readonly IHttpContextAccessor _httpContext;

        public PermissionHelper(IConfiguration config,IUserRepository userRepo,IHttpContextAccessor httpContext)
        {
            _config = config;
            _userRepo = userRepo;
            _httpContext = httpContext;
        }
        public async Task<bool> CheckPermission(string configKey)
        {
            // 1. read key from appsettings
            string keyFromConfig = _config[$"PermissionKeys:{configKey}"];
            // 2. get userId from JWT
            //var userId = int.Parse(_httpContext.HttpContext.User.FindFirst("UserId").Value);
            var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new InvalidOperationException("UserId claim not found in JWT.");
            }
            var userId = int.Parse(userIdClaim.Value);
            // 3. call repo → match config key with DB permissionName
            return await _userRepo.UserHasPermission(userId, keyFromConfig);
        }
    }

}
