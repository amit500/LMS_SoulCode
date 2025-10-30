using LMS_SoulCode.Features.Auth.Entities;

namespace LMS_SoulCode.Features.UserPermissions.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

    }
}
