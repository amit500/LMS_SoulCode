using LMS_SoulCode.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS_SoulCode.Common
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CheckPermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _configKey;

        public CheckPermissionAttribute(string configKey)
        {
            _configKey = configKey;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var permissionHelper = context.HttpContext
                                          .RequestServices
                                          .GetRequiredService<PermissionHelper>();

            bool isAllowed = await permissionHelper.CheckPermission(_configKey);

            if (!isAllowed)
            {
                context.Result = new ForbidResult("Permission Denied");
            }
        }
    }
}
