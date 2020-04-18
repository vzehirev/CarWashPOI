using CarWashPOI.Data.Models;
using CarWashPOI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommentsService commentsService;

        public CommentsController(UserManager<ApplicationUser> userManager, ICommentsService commentsService)
        {
            this.userManager = userManager;
            this.commentsService = commentsService;
        }

        [HttpPost]
        public async Task<IActionResult> CommentLocation(int locationId, string comment)
        {
            string userId = userManager.GetUserId(User);

            int result = await commentsService.AddLocationCommentAsync(userId, locationId, comment);

            if (result > 0)
            {
                TempData["CommentSuccess"] = true;
            }

            return LocalRedirect($"/Locations/{locationId}#commentSuccess");
        }
    }
}