using LMS_SoulCode.Features.UserPermissions.Entities;

namespace LMS_SoulCode.Features.UserPermissions.Models
{
    public record RoleRequest
    {
        public string Name { get; set; } = string.Empty;
        public List<RolePermission>? RolePermissions { get; set; }
    }
}
