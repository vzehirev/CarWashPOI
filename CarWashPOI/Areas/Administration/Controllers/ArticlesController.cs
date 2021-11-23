using CarWashPOI.Services.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWashPOI.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public async Task<IActionResult> ForApproval(int id)
        {
            CarWashPOI.ViewModels.Articles.ReadArticleOutputModel outputModel = await articlesService.GetArticleByIdAsAdminAsync(id);

            return View(outputModel);
        }

        public async Task<IActionResult> Approve(int id)
        {
            int result = await articlesService.ApproveArticleAsync(id);

            if (result > 0)
            {
                TempData["articleApproved"] = true;
            }

            return RedirectToAction("Index", "Home", new { area = "Administration" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            int result = await articlesService.DeleteArticleAsync(id);

            if (result > 0)
            {
                TempData["articleDeleted"] = true;
            }

            return RedirectToAction("Index", "Home", new { area = "Administration" });
        }
    }
}