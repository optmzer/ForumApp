using ForumApp.Data;
using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
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
            // Check if I get correct int postId
            return _context.Posts
                .Where(post => post.Id == postId)
                .Include(post => post.User)
                .Include(post => post.Forum)
                .Include(post => post.Replies)
                    .ThenInclude(reply => reply.User)
                //Because in PostController we use reply.User.UserName
                // In BuildPostRepliesModel()
                .First();
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
