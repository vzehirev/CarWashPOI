using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.Services;
using CarWashPOI.Services.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWashPOI.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "admin")]
    public class LocationsController : Controller
    {
        private readonly ILocationsService locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            this.locationsService = locationsService;
        }

        public async Task<IActionResult> ForApproval(int id)
        {
            var outputModel = await locationsService.GetLocationDetailsAsync(id);

            return View(outputModel);
        }

        public async Task<IActionResult> Approve(int id)
        {
            int result = await locationsService.ApproveLocationAsync(id);

            if (result > 0)
            {
                TempData["locationApproved"] = true;
            }

            return RedirectToAction("Index", "Home", new { area = "Administration" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            int result = await locationsService.DeleteLocationAsync(id);

            if (result > 0)
            {
                TempData["locationDeleted"] = true;
            }

            return RedirectToAction("Index", "Home", new { area = "Administration" });
        }
    }
}