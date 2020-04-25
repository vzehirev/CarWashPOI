using CarWashPOI.Services.Emails;
using CarWashPOI.ViewModels.SiteInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers
{
    public class SiteInfoController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration configuration;
        private readonly IEmailsService emailsService;

        public SiteInfoController(IWebHostEnvironment env, IConfiguration configuration, IEmailsService emailsService)
        {
            this.env = env;
            this.configuration = configuration;
            this.emailsService = emailsService;
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

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactUsInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            SendGrid.Response result = await emailsService
                .SendAsync(configuration["SendGrid:To"], $"From: {inputModel.Email}", inputModel.Message);

            if (result.StatusCode.ToString() == "Accepted")
            {
                TempData["contact"] = true;

                return RedirectToAction(nameof(ContactUs));
            }
            else
            {
                TempData["contact"] = false;

                return View(inputModel);
            }

        }
    }
}