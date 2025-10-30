namespace LMS_SoulCode.Features.UserPermissions.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<RolePermission>? RolePermissions { get; set; }

    }
}
