using LMS_SoulCode.Data;
using LMS_SoulCode.Features.UserPermissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.UserPermissions.Repositories

{
    public class RoleRepository : IRoleRepository
    {
        private readonly LmsDbContext _context;
        public RoleRepository(LmsDbContext context) => _context = context;

        public async Task<IEnumerable<object>> GetAllAsync()
                        => await _context.Roles.Select(r => new {
                            r.Id,
                            RoleName = r.Name                         
                        }).ToListAsync();

        public async Task<IEnumerable<object>> GetAllRolePermissionAsync()
                     => await _context.RolePermissions.Select(rp => new
                     {
                         rp.RoleId,
                         RoleName = rp.Role.Name,
                         rp.PermissionId,
                         PermissionName = rp.Permission.Name
                     }).ToListAsync();        

        public async Task<Role?> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);


        public async Task<Role> AddAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
