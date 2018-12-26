using ForumApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ForumApp.Data;
using ForumApp.Models.ApplicationUser;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace ForumApp.Controllers
{


    public class UserProfileController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string AzureBlobStorageConnection;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;

        public UserProfileController(
            UserManager<ApplicationUser> userManager
            , IApplicationUser userService
            , IUpload uploadService
            , IConfiguration config)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _config = config;
            AzureBlobStorageConnection = _config.GetConnectionString("AzureBlobStorageConnection");
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
            };

            return View(model);
        }

        /*
         * Uploads User profile image to Azure Blob storage
         */ 
        [HttpPost]
        public async Task<IActionResult> UploadUserProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);
            var container = _uploadService.GetStorageContainer(AzureBlobStorageConnection);
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = contentDisposition.FileName.Trim('"');
            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _userService.SetProfileImageAsync(userId, blockBlob.Uri);

            return RedirectToAction("UserDetails", "UserProfile", new { userId });
        }
    }
}