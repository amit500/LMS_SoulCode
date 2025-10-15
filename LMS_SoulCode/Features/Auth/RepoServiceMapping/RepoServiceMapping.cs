using LMS_SoulCode.Features.Auth.Repositories;
using LMS_SoulCode.Features.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LMS_SoulCode.Features.Auth.RepositoryMapping
{
    public static class RepoServiceMapping
    {
        public static void AddRepoServiceMapping(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
