using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.Services;
using CarWashPOI.ViewModels.Locations;
using Microsoft.AspNetCore.Mvc;

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

        [Route("/Locations/{id}")]
        public async Task<IActionResult> LocationDetails(int id)
        {
            var outputModel = await locationsService.GetLocationDetailsAsync(id);
            return View(outputModel);
        }

        public async Task<IActionResult> Add()
        {
            var addLocationViewModel = new AddLocationViewModel();
            addLocationViewModel.AllLocationTypes = await locationTypesService.GetAllLocationTypesAsync();
            addLocationViewModel.AllTowns = await townsService.GetAllTownsAsync();

            return View(addLocationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLocationViewModel addLocationViewModel)
        {
            if (ModelState.IsValid)
            {
                var locationId = await locationsService.AddAsync(addLocationViewModel);
                return View();
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