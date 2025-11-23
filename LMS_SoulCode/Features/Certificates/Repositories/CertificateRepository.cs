using LMS_SoulCode.Data;
using LMS_SoulCode.Features.Certificates.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.Certificates.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly LmsDbContext _context;

        public CertificateRepository(LmsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Certificate cert)
        {
            await _context.Certificates.AddAsync(cert);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Certificate>> GetAllAsync()
        {
            return await _context.Certificates
                .OrderByDescending(x => x.IssuedAt)
                .ToListAsync();
        }

        public async Task<Certificate?> GetByIdAsync(int id)
        {
            return await _context.Certificates
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Certificate?> GetByCodeAsync(string code)
        {
            return await _context.Certificates
                .FirstOrDefaultAsync(x => x.CertificateCode == code);
        }

        public async Task<IEnumerable<Certificate>> GetByUserIdAsync(int userId)
        {
            return await _context.Certificates
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.IssuedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(Certificate cert)
        {
            _context.Certificates.Update(cert);
            await _context.SaveChangesAsync();
        }
    }
}
