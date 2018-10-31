﻿using ForumApp.Data.Models;
using System;
using System.Threading.Tasks;

namespace ForumApp.Data
{
    public interface IForum
    {
        Forum GetById(int forumId);
        IEquatable<Forum> GetAll();
        IEquatable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
    }
}
