namespace LMS_SoulCode.Features.UserPermissions.Models
{

        public record CreatePermissionRequest
        {
            public string PermissionName { get; set; } = string.Empty;
        }
    
}
