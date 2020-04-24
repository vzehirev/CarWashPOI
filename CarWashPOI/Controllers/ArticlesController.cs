using CarWashPOI.Data.Models;
using CarWashPOI.Services.Articles;
using CarWashPOI.ViewModels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesController(IArticlesService articlesService, UserManager<ApplicationUser> userManager)
        {
            this.articlesService = articlesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page, string orderBy)
        {
            const int ResultsPerPage = 10;

            if (page < 1)
            {
                page = 1;
            }

            int skip = (page - 1) * ResultsPerPage;

            ArticlesIndexOutputModel outputModel = await articlesService.GetArticlesAsync(skip, ResultsPerPage, orderBy);

            if (outputModel.AllArticles == 0)
            {
                return View(outputModel);
            }

            int lastPage = (int)Math.Ceiling(((double)outputModel.AllArticles / ResultsPerPage));

            if (page > lastPage)
            {
                page = lastPage;

                return RedirectToAction(nameof(Index), outputModel);
            }

            outputModel.CurrentPage = page;
            outputModel.LastPage = lastPage;

            return View(outputModel);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Route("/Articles/{articleId:int}")]
        public async Task<IActionResult> ReadArticle(int articleId)
        {
            ReadArticleOutputModel article = await articlesService.GetArticleByIdAsUserAsync(articleId);

            return View(article);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddArticleViewModel inputModel)
        {
            inputModel.UserId = userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            int articleId = await articlesService.AddArticleAsync(inputModel);

            if (articleId > 0)
            {
                TempData["articleAdded"] = true;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}