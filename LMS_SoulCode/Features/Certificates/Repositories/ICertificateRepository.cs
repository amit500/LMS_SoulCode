using LMS_SoulCode.Features.Certificates.Models;
using System;
namespace LMS_SoulCode.Features.Certificates.Repositories
{
    public interface ICertificateRepository
    {
        Task<Certificate?> GetByIdAsync(int id);
        Task<Certificate?> GetByCodeAsync(string code);
        Task AddAsync(Certificate cert);
        Task UpdateAsync(Certificate cert);
        Task<IEnumerable<Certificate>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Certificate>> GetAllAsync();

    }
}
