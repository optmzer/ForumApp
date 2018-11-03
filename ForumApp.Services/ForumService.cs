using ForumApp.Data;
using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;
        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Querying DB for actuall data.
        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums
                .Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int forumId)
        {
            var forum = _context.Forums
                .Where(f => f.Id == forumId)
                .Include(f => f.Posts)
                    .ThenInclude(p => p.User) //Post property + Creator of the post User
                .Include(f => f.Posts)
                    .ThenInclude(p => p.Replies) //Include Replies and their creators
                        .ThenInclude(r => r.User)
                 .FirstOrDefault(); //To make sure we are not returning NULL

            return forum;
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
