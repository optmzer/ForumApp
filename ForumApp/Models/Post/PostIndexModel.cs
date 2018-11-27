using ForumApp.Models.Forum;
using ForumApp.Models.PostReply;
using System.Collections.Generic;

namespace ForumApp.Models.Post
{
    public class PostIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Created { get; set; }
        public string Content { get; set; }

        // Author data
        public bool IsAuthorAdmin { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImgUrl { get; set; }
        public string AuthorId { get; set; }

        // Forum Listing Model - Data about the forum
        public ForumListingModel Forum { get; set; }

        // Post replies
        public int RepliesCount { get; set; }
        public IEnumerable<PostReplyModel> PostReplies { get; set; }
    }
}
