using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.ViewModels.SiteInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarWashPOI.Controllers
{
    public class SiteInfoController : Controller
    {
        private readonly IWebHostEnvironment env;

        public SiteInfoController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<IActionResult> PrivacyPolicy()
        {
            var filePath = env.ContentRootPath + Path.DirectorySeparatorChar + "privacyPolicy.txt";
            var outputModel = new PrivacyOutputModel
            {
                Content = await System.IO.File.ReadAllTextAsync(filePath),
            };
            return View(outputModel);
        }

        public async Task<IActionResult> TermsAndConditions()
        {
            var filePath = env.ContentRootPath + Path.DirectorySeparatorChar + "termsAndConditions.txt";
            var outputModel = new TermsAndConditionsOutputModel
            {
                Content = await System.IO.File.ReadAllTextAsync(filePath),
            };
            return View(outputModel);
        }
    }
}