using CarWashPOI.Data.Models;
using CarWashPOI.Services.Comments;
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
            const int CommentMinLen = 3;

            string userId = userManager.GetUserId(User);

            if (comment.Length > CommentMinLen)
            {
                int result = await commentsService.AddLocationCommentAsync(userId, locationId, comment);

                if (result > 0)
                {
                    TempData["CommentSuccess"] = true;
                }
            }
            else
            {
                TempData["CommentSuccess"] = false;
            }

            return LocalRedirect($"/Locations/{locationId}#commentSuccess");
        }
    }
}