using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWashPOI.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public async Task<IActionResult> ForApproval(int id)
        {
            var outputModel = await articlesService.GetArticleByIdAsync(id);

            return View(outputModel);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var result = await articlesService.ApproveArticleAsync(id);

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