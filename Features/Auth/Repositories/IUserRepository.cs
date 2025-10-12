using LMS_SoulCode.Features.Auth.Entities;

namespace LMS_SoulCode.Features.Auth.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameOrEmailAsync(string Email);
        Task AddAsync(User user);

    }
}
