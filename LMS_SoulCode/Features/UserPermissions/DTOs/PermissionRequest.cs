namespace LMS_SoulCode.Features.UserPermissions.DTOs
{

        public record CreatePermissionRequest
        {
            public string PermissionName { get; set; } = string.Empty;
        }
    
}
