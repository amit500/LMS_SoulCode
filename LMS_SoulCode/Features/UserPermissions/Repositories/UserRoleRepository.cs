using LMS_SoulCode.Data;
using LMS_SoulCode.Features.UserPermissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.UserPermissions.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly LmsDbContext _context;

        public UserRoleRepository(LmsDbContext context)
            => _context = context;

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                throw new Exception($"User with Id {userId} not found.");

            var roleExists = await _context.Roles.AnyAsync(r => r.Id == roleId);
            if (!roleExists)
                throw new Exception($"Role with Id {roleId} not found.");

            var alreadyAssigned = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (alreadyAssigned)
                throw new Exception($"Role with Id {roleId} is already assigned to User with Id {userId}.");

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                throw new Exception($"User with Id {userId} not found.");

            var roleExists = await _context.Roles.AnyAsync(r => r.Id == roleId);
            if (!roleExists)
                throw new Exception($"Role with Id {roleId} not found.");

            var alreadyAssigned = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetUserRolesAsync(int userId)
            => await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                 .Select(ur => ur.Role.Name)
                .ToListAsync();
        public async Task<bool> UserHasPermission(int userId, string key)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .AnyAsync(rp => rp.Permission.Name == key);
        }


    }


}
