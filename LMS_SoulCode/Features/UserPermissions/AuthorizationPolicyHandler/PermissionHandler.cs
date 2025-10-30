using LMS_SoulCode.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS_SoulCode.Features.UserPermissions.AuthorizationPolicyHandler
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly LmsDbContext _context;

        public PermissionHandler(LmsDbContext context)
        {
            _context = context;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userEmail = context.User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
                return;

            var hasPermission = await (
                from user in _context.Users
                join ur in _context.UserRoles on user.Id equals ur.UserId
                join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                join p in _context.Permissions on rp.PermissionId equals p.Id
                where user.Email == userEmail && p.Name == requirement.PermissionName
                select p
            ).AnyAsync();

            if (hasPermission)
                context.Succeed(requirement);
        }
    }
}
