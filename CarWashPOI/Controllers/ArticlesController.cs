using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.Data.Models;
using CarWashPOI.Services;
using CarWashPOI.ViewModels.Articles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            const int resultsPerPage = 10;

            if (page < 1)
            {
                page = 1;
            }

            int skip = (page - 1) * resultsPerPage;

            var outputModel = await articlesService.GetArticlesAsync(skip, resultsPerPage, orderBy);

            if (outputModel.AllArticles == 0)
            {
                return View(outputModel);
            }

            int lastPage = (int)Math.Ceiling(((double)outputModel.AllArticles / resultsPerPage));

            if (page > lastPage)
            {
                page = lastPage;

                return RedirectToAction(nameof(Index), outputModel);
            }

            outputModel.CurrentPage = page;
            outputModel.LastPage = lastPage;
            return View(outputModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [Route("/Articles/{articleId:int}")]
        public async Task<IActionResult> ReadArticle(int articleId)
        {
            var article = await articlesService.GetArticleByIdAsync(articleId);

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddArticleViewModel inputModel)
        {
            inputModel.UserId = userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var articleId = await articlesService.AddArticleAsync(inputModel);

            if (articleId > 0)
            {
                TempData["articleAdded"] = true;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}