using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EStore.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed the roles (Admin and User)
            var adminRoleId = "778c187c-c0fc-4917-9868-69e720c14ac2";
            var userRoleId = "57971bda-1908-4fc7-af42-7f32964bb2d9";            
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);


            //seed the admin user
            var adminId = "6cd09cc0-de01-4214-b872-a907284595ed";
            var adminUser = new IdentityUser() 
            { 
                Id = adminId,
                UserName = "admin",
                Email = "admin@estore.com",
                NormalizedEmail = "admin@estore.com".ToUpper(),
                NormalizedUserName = "ADMIN",
            };
            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "password123");
            builder.Entity<IdentityUser>().HasData(adminUser);


            //Add roles to the admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = adminId,
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }



    }
}
