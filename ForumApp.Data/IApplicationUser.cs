using ForumApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApp.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string userId);
        IEnumerable<ApplicationUser> GetAll();
        Task SetProfileImageAsync(string userId, Uri uri);
        Task UpdateUserRating(string userId, Type type);
    }
}
