using System;
using System.Collections.Generic;

namespace ForumApp.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        //Later on can add upload for pictures

        //ApplicationUser Identiies a user who made the post
        public virtual ApplicationUser User { get; set; }
        //public virtual IdentityUser User { get; set; }
        public virtual Forum Forum { get; set; }

        //Replies
        public virtual IEnumerable<PostReply> Replies { get; set; }
    }
}
