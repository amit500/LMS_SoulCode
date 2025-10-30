using LMS_SoulCode.Features.Auth.Repositories;
using LMS_SoulCode.Features.Auth.Services;
using LMS_SoulCode.Features.UserPermissions.Repositories;
using LMS_SoulCode.Features.UserPermissions.Services;

namespace LMS_SoulCode.RepositoryMapping
{
    public static class RepoServiceMapping
    {
        public static void AddRepoServiceMapping(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IEmailService, EmailService>();


        }
    }
}
