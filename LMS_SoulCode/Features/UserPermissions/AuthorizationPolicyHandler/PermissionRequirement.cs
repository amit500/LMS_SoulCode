using Microsoft.AspNetCore.Authorization;

namespace LMS_SoulCode.Features.UserPermissions.AuthorizationPolicyHandler
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }

        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
