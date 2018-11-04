using ForumApp.Data.Models;
using System;
using System.Collections.Generic;

namespace ForumApp.Models.Forum
{
    public class ForumPostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        //Topic User
        public ApplicationUser User { get; set; }

        //Post replies
        public IEnumerable<PostReply> Replies { get; set; }
    }
}
