using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ITHSManagement.Models;
using ITHSManagement.Models.Entities;

namespace ITHSManagement.Data
{
    public class EFContext : IdentityDbContext<IdentityUser>, IEFContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            const string roleId = "root-0c0-aa65-4af8-bd17-00bd9344e575";
            const string userRoleId = "user-2c0-aa65-4af8-bd17-00bd9344e575";
            const string adminId = "admin-c0-aa65-4af8-bd17-00bd9344e575";
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId,
                Name = "root",
                NormalizedName = "ROOT"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER"
            });
            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@iths.se",
                NormalizedEmail = "ADMIN@ITHS.SE",
                EmailConfirmed = true,
                LockoutEnabled = true,
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = System.Guid.NewGuid().ToString(),

            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId

            }) ;
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<LIAWork> LIAWork { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompRep> CompRep { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}