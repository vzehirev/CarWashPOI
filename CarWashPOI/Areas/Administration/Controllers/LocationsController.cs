﻿using CarWashPOI.Services.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWashPOI.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class LocationsController : Controller
    {
        private readonly ILocationsService locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            this.locationsService = locationsService;
        }

        public async Task<IActionResult> ForApproval(int id)
        {
            CarWashPOI.ViewModels.Locations.LocationDetailsOutputModel outputModel = await locationsService.GetLocationDetailsAsAdminAsync(id);

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