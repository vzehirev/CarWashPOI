using CarWashPOI.Data.Models;
using CarWashPOI.Services;
using CarWashPOI.Services.Locations;
using CarWashPOI.Services.LocationTypes;
using CarWashPOI.Services.Towns;
using CarWashPOI.ViewModels.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationsService locationsService;
        private readonly ITownsService townsService;
        private readonly ILocationTypesService locationTypesService;

        public LocationsController(ILocationsService locationsService, ITownsService townsService,
            ILocationTypesService locationTypesService)
        {
            this.locationsService = locationsService;
            this.townsService = townsService;
            this.locationTypesService = locationTypesService;
        }

        [Route("/Locations/{id:int}")]
        public async Task<IActionResult> LocationDetails(int id)
        {
            LocationDetailsOutputModel outputModel = await locationsService.GetLocationDetailsAsync(id);
            outputModel.Comments = outputModel.Comments.OrderByDescending(c => c.AddedOn);
            return View(outputModel);
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            AddLocationViewModel addLocationViewModel = new AddLocationViewModel
            {
                AllLocationTypes = await locationTypesService.GetAllLocationTypesAsync(),
                AllTowns = await townsService.GetAllTownsAsync()
            };

            return View(addLocationViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddLocationViewModel addLocationViewModel)
        {
            if (!ModelState.IsValid)
            {
                addLocationViewModel.AllLocationTypes = await locationTypesService.GetAllLocationTypesAsync();
                addLocationViewModel.AllTowns = await townsService.GetAllTownsAsync();
                return View(addLocationViewModel);
            }

            int locationId = await locationsService.AddAsync(addLocationViewModel);
            if (locationId > 0)
            {
                TempData["locationAdded"] = true;
            }
            return LocalRedirect("/#locationAdded");
        }
    }
}