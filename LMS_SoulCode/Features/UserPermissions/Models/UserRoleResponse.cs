namespace LMS_SoulCode.Features.UserPermissions.Models
{
    public class UserRoleResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
