using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.Areas.Administration.ViewModels;
using CarWashPOI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWashPOI.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly ILocationsService locationsService;

        public HomeController(IArticlesService articlesService, ILocationsService locationsService)
        {
            this.articlesService = articlesService;
            this.locationsService = locationsService;
        }
        public async Task<IActionResult> Index()
        {
            var outputModel = new AdministrationIndexOutputModel
            {
                LocationsForApproval = await locationsService.GetNonApprovedLocationsAsync(),
                ArticlesForApproval = await articlesService.GetNonApprovedArticlesAsync(),
            };

            return View(outputModel);
        }
    }
}