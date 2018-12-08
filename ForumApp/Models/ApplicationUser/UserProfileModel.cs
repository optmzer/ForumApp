using Microsoft.AspNetCore.Http;
using System;

namespace ForumApp.Models.ApplicationUser
{
    public class UserProfileModel
    {
        public bool IsAdmin { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserRating { get; set; }
        public string UserProfileImageUrl { get; set; }

        public DateTime UserProfileCreated { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
