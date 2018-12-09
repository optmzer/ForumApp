using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ForumApp.Data;
using ForumApp.Models.ApplicationUser;

namespace ForumApp.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;

        public UserProfileController(
            UserManager<ApplicationUser> userManager
            , IApplicationUser userService
            , IUpload uploadService)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
        }

        public IActionResult UserDetails(string userId)
        {
            var user = _userService.GetById(userId);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new UserProfileModel
            {
                IsAdmin = userRoles.Contains("Admin"),
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                UserProfileImageUrl = user.ProfileImageUrl,
                UserProfileCreated = user.MemberSince,
                //ImageUpload =
            };

            return View(model);
        }

        /*
         * Uploads User profile image to Azure Blob storage
         */ 
        [HttpPost]
        public IActionResult UploadUserProfileImage()
        {
            return View();
        }
    }
}