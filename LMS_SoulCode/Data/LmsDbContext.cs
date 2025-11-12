using LMS_SoulCode.Features.Auth.Entities;
using LMS_SoulCode.Features.UserPermissions.Entities;
using LMS_SoulCode.Features.Course.Entities;
using LMS_SoulCode.Features.CourseVideos.Entities;
using Microsoft.EntityFrameworkCore;
using LMS_SoulCode.Features.SubscribedCourse.Entities;

namespace LMS_SoulCode.Data
{
    public class LmsDbContext:DbContext
    {
        public LmsDbContext(DbContextOptions<LmsDbContext> options)
    : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);


            modelBuilder.Entity<UserCourse>()
           .HasKey(uc => new { uc.UserId, uc.CourseId });

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany() // or define collection property on User if present
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany() // or define collection property on Course if present
                .HasForeignKey(uc => uc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // optional: map SubscribedAt default SQL value
            modelBuilder.Entity<UserCourse>()
                .Property(uc => uc.SubscribedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
