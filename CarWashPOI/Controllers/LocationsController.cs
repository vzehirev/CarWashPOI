using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.ViewModels.Location.Input;
using Microsoft.AspNetCore.Mvc;

namespace CarWashPOI.Controllers
{
    public class LocationsController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddLocationInputModel inputModel)
        {

            return View();
        }
    }
}