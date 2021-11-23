using CarWashPOI.Areas.Administration.ViewModels;
using CarWashPOI.Services.Articles;
using CarWashPOI.Services.Locations;
using CarWashPOI.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWashPOI.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly ILocationsService locationsService;
        private readonly IUsersService usersService;

        public HomeController(IArticlesService articlesService,
            ILocationsService locationsService,
            IUsersService usersService)
        {
            this.articlesService = articlesService;
            this.locationsService = locationsService;
            this.usersService = usersService;
        }
        public async Task<IActionResult> Index()
        {
            AdministrationIndexOutputModel outputModel = new AdministrationIndexOutputModel
            {
                LocationsForApproval = await locationsService.GetNonApprovedLocationsAsync(),
                ArticlesForApproval = await articlesService.GetNonApprovedArticlesAsync(),
                TotalLocations = await locationsService.GetTotalLocationsAsync(),
                TotalArticles = await articlesService.GetTotalArticlesAsync(),
                TotalUsers = await usersService.GetTotalUsersAsync(),
            };

            return View(outputModel);
        }
    }
}