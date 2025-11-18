using LMS_SoulCode.Features.Auth.Repositories;
using LMS_SoulCode.Features.Auth.Services;
using LMS_SoulCode.Features.UserPermissions.Repositories;
using LMS_SoulCode.Features.UserPermissions.Services;
using LMS_SoulCode.Features.Course.Repositories;
using LMS_SoulCode.Features.Course.Services;
using LMS_SoulCode.Features.CourseVideos.Repositories;
using LMS_SoulCode.Features.CourseVideos.Services;
using LMS_SoulCode.Features.SubscribedCourse.Repositories;
using LMS_SoulCode.Features.SubscribedCourse.Services;
using LMS_SoulCode.Features.Security.Services;
using LMS_SoulCode.Features.Reports.Services;


namespace LMS_SoulCode.RepositoryMapping
{
    public static class RepoServiceMapping
    {
        public static void AddRepoServiceMapping(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IEmailService, EmailService>();    
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseVideoRepository, CourseVideoRepository>();
            services.AddScoped<CourseVideoService>();
            services.AddScoped<IUserCourseRepository, UserCourseRepository>();
            services.AddScoped<IUserCourseService, UserCourseService>();
            services.AddScoped<CryptographyService>();
            services.AddScoped<CourseReportService>();


        }
    }
}
