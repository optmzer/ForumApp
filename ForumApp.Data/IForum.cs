using ForumApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApp.Data
{
    public interface IForum
    {
        //Describing methods we are going to use.
        Forum GetById(int forumId);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
    }
}
