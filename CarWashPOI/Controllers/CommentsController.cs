using CarWashPOI.Data.Models;
using CarWashPOI.Services.Comments;
using CarWashPOI.ViewModels.Comments;
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
        public async Task<IActionResult> CommentLocation(CommentInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["commentSuccess"] = false;

                return LocalRedirect($"/Locations/{inputModel.LocationId}#commentSuccess");
            }

            inputModel.UserId = userManager.GetUserId(User);

            int result = await commentsService.AddLocationCommentAsync(inputModel);

            if (result > 0)
            {
                TempData["commentSuccess"] = true;
            }

            return LocalRedirect($"/Locations/{inputModel.LocationId}#commentSuccess");
        }
    }
}