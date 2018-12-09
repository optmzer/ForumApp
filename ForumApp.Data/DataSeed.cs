using ForumApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Creates a SuperUser on application start up
 * if it is not created yet.
 */

namespace ForumApp.Data
{
    public class DataSeed
    {
        private readonly ApplicationDbContext _context;

        public DataSeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateSuperUser()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var userStore = new UserStore<ApplicationUser>(_context);

            var pswdHasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser
            {
                UserName = "ForumAdmin",
                NormalizedUserName = "forumadmin",
                Email = "admin@domain.com",
                NormalizedEmail = "admin@domain.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Hash the password
            var hashedPassword = pswdHasher.HashPassword(user, "admin@12345");
            // update the user with new password hash
            user.PasswordHash = hashedPassword;

            // Check if role exists
            var adminRoleExists = _context.Roles.Any(roles => 
                                            roles.Name == "Admin");
            // Create role Admin if it does not
            if (!adminRoleExists)
            {
                roleStore.CreateAsync(new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "admin"
                    }
                );
            }

            // Check if a DB has a SuperUser with name Admin
            var superUserRoleExists = _context.Users.Any(
                u => u.NormalizedUserName == user.NormalizedUserName);

            // Create SuperUser if it does not exits
            // The add Admin role to DB
            if (!superUserRoleExists)
            {
                userStore.CreateAsync(user);
                userStore.AddToRoleAsync(user, "Admin");
            }

            _context.SaveChangesAsync();

            return Task.CompletedTask;

        } // CreateSuperUser()


    }
}
