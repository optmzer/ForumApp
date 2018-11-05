using ForumApp.Data;
using ForumApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int postId)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int postId, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetFilteredPosts(int searchQuery)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsByForum(int forumId)
        {
            return _context.Forums
                .Where(forum => forum.Id == forumId)
                .First()
                .Posts;
        }
    }
}
