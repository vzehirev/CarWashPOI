using CarWashPOI.ViewModels.SiteInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

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
            string filePath = env.ContentRootPath + Path.DirectorySeparatorChar + "privacyPolicy.txt";
            PrivacyOutputModel outputModel = new PrivacyOutputModel
            {
                Content = await System.IO.File.ReadAllTextAsync(filePath),
            };

            return View(outputModel);
        }

        public async Task<IActionResult> TermsAndConditions()
        {
            string filePath = env.ContentRootPath + Path.DirectorySeparatorChar + "termsAndConditions.txt";
            TermsAndConditionsOutputModel outputModel = new TermsAndConditionsOutputModel
            {
                Content = await System.IO.File.ReadAllTextAsync(filePath),
            };

            return View(outputModel);
        }
    }
}