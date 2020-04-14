using CarWashPOI.Services;
using CarWashPOI.ViewModels.Locations;
using Microsoft.AspNetCore.Mvc;
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
            return View(outputModel);
        }

        public async Task<IActionResult> Add()
        {
            AddLocationViewModel addLocationViewModel = new AddLocationViewModel
            {
                AllLocationTypes = await locationTypesService.GetAllLocationTypesAsync(),
                AllTowns = await townsService.GetAllTownsAsync()
            };

            return View(addLocationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLocationViewModel addLocationViewModel)
        {
            if (ModelState.IsValid)
            {
                int locationId = await locationsService.AddAsync(addLocationViewModel);
                return RedirectToAction(nameof(LocationDetails), new { id = locationId });
            }
            else
            {
                addLocationViewModel.AllLocationTypes = await locationTypesService.GetAllLocationTypesAsync();
                addLocationViewModel.AllTowns = await townsService.GetAllTownsAsync();
                return View(addLocationViewModel);
            }
        }
    }
}