using Microsoft.AspNetCore.Identity;
using System;

namespace ForumApp.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; } = 0;
        public string ProfileImageUrl { get; set; } = "/images/forum/default_avatar.png";
        public DateTime MemberSince { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}