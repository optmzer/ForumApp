using System.Collections.Generic;
/**
 * Wrapper for UserProfileModel 
 */
namespace ForumApp.Models.ApplicationUser
{
    public class UserProfileListModel
    {
        public IEnumerable<UserProfileModel> UserProfiles { get; set; }
    }
}
