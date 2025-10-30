using LMS_SoulCode.Features.UserPermissions.AuthorizationPolicyHandler;
using Microsoft.AspNetCore.Authorization;

namespace LMS_SoulCode.Features.UserPermissions.AuthorizationPoliciesMapping
{
    public static class AuthorizationMapping
    {
        public static void AddUserPolicies(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("View Courses", policy =>
                    policy.Requirements.Add(new PermissionRequirement("View Courses")));

                options.AddPolicy("Edit Course", policy =>
                    policy.Requirements.Add(new PermissionRequirement("Edit Course")));

            });
        }
    }
}
