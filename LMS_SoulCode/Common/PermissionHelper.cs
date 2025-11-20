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
            string keyFromConfig = _config[$"PermissionKeys:{configKey}"];

            var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return false;
            int userId = int.Parse(userIdClaim.Value);

            return await _userRepo.UserHasPermission(userId, keyFromConfig);
        }

    }

}
