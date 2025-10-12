using LMS_SoulCode.Features.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Data
{
    public class LmsDbContext:DbContext
    {
        public LmsDbContext(DbContextOptions<LmsDbContext> options)
    : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
