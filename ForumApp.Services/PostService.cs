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

        public async Task AddNewPostReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public async Task AddPost(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
            //_context.
              int id =  post.Id;
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
        { // Same includes as in GetPostById()
            return _context.Posts
                .Include(post => post.User)
                .Include(post => post.Forum)
                .Include(post => post.Replies)
                    .ThenInclude(reply => reply.User);
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery) 
                ? forum.Posts
                : forum.Posts
                    .Where(post => post.Title.ToLower().Contains(searchQuery.ToLower())
                                || post.Content.ToLower().Contains(searchQuery.ToLower()));
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? GetAll()
                : GetAll().Where(post => post.Title.ToLower().Contains(searchQuery.ToLower())
                                      || post.Content.ToLower().Contains(searchQuery.ToLower()));
        }

        public IEnumerable<Post> GetLatestPosts(int numOfPosts)
        {
            return GetAll()
                .OrderByDescending(post => post.Created)
                .Take(numOfPosts);
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
