using CarWashPOI.Services.Locations;
using CarWashPOI.Services.LocationTypes;
using CarWashPOI.Services.Towns;
using CarWashPOI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocationsService locationsService;
        private readonly ITownsService townsService;
        private readonly ILocationTypesService locationTypesService;

        public HomeController(ILocationsService locationsService, ITownsService townsService, ILocationTypesService locationTypesService)
        {
            this.locationsService = locationsService;
            this.townsService = townsService;
            this.locationTypesService = locationTypesService;
        }

        public async Task<IActionResult> Index(HomePageInputModel inputModel)
        {
            const int ResultsPerPage = 9;

            if (inputModel.Page < 1)
            {
                inputModel.Page = 1;
            }

            int skip = (inputModel.Page - 1) * ResultsPerPage;

            HomePageOutputModel homePageOutputModel = await locationsService.GetLocationsAsync(
                    inputModel.TownId,
                    inputModel.TypeId,
                    inputModel.OrderBy,
                    skip,
                    ResultsPerPage);

            homePageOutputModel.AllTowns = await townsService.GetAllTownsAsync();
            homePageOutputModel.AllTypes = await locationTypesService.GetAllLocationTypesAsync();
            homePageOutputModel.SelectedTownId = inputModel.TownId;
            homePageOutputModel.SelectedTypeId = inputModel.TypeId;
            homePageOutputModel.SelectedOrderBy = inputModel.OrderBy;

            if (homePageOutputModel.AllLocations == 0)
            {
                return View(homePageOutputModel);
            }

            int lastPage = (int)Math.Ceiling(((double)homePageOutputModel.AllLocations / ResultsPerPage));

            if (inputModel.Page > lastPage)
            {
                inputModel.Page = lastPage;

                return RedirectToAction(nameof(Index), inputModel);
            }

            homePageOutputModel.CurrentPage = inputModel.Page;
            homePageOutputModel.LastPage = lastPage;

            return View(homePageOutputModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
