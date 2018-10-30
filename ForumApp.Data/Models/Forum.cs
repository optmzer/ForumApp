﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ForumApp.Data.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }

        //Collection of posts
        public virtual IEnumerable<Post> Posts { get; set; }


    }
}
