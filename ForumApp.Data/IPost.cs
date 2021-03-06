﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApp.Data.Models
{
    public interface IPost
    {
        Post GetPostById(int postId);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int forumId);
        IEnumerable<Post> GetLatestPosts(int numOfPosts);

        Task AddPost(Post post);
        Task Delete(int postId);
        Task EditPostContent(int postId, string newContent);
        Task AddNewPostReply(PostReply reply);
    }
}
