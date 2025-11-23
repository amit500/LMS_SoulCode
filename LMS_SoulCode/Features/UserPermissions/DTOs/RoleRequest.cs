using LMS_SoulCode.Features.UserPermissions.Models;

namespace LMS_SoulCode.Features.UserPermissions.DTOs
{
    public record RoleRequest
    {
        public string RoleName { get; set; } = string.Empty;
        public List<RolePermission>? RolePermissions { get; set; }
        public List<int>? PermissionIds { get; set; }

    }
    public class CreateRoleRequest
    {
        public string RoleName { get; set; } = string.Empty;
        public List<int>? PermissionIds { get; set; }
    }
}
