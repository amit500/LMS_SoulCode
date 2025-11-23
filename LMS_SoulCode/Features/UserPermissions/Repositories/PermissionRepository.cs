using LMS_SoulCode.Data;
using LMS_SoulCode.Features.UserPermissions.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.UserPermissions.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly LmsDbContext _context;
        public PermissionRepository(LmsDbContext context) => _context = context;

        public async Task<IEnumerable<object>> GetAllAsync()
            => await _context.Permissions.Select(p=> new {p.Id,PermissionName = p.Name}).ToListAsync();

        public async Task<Permission?> GetByIdAsync(int id)
            => await _context.Permissions.FindAsync(id);

        public async Task<Permission> AddAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Permission permission)
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }
    }
}
