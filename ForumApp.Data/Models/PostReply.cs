using System;

namespace ForumApp.Data.Models
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        //Who reply belongs to
        //virtual is a Foreighn Key
        public virtual ApplicationUser User { get; set; }
        //To what Post it belongs to
        public virtual Post Post { get; set; }
    }
}
