using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApp.Data;
using ForumApp.Data.Models;

/**
 Manages User data and Identities
     */

namespace ForumApp.Services
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public ApplicationUser GetById(string userId)
        {
            return GetAll().FirstOrDefault(user => user.Id == userId);
        }

        public Task IncrementRating(string userId, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImageAsync(string userId, Uri uri)
        {
            var user = GetById(userId);
            user.ProfileImageUrl = uri.AbsoluteUri;

            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
