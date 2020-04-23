using CarWashPOI.Data.Models;
using CarWashPOI.Services.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CarWashPOI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IEmailsService emailsService;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IEmailsService emailsService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            this.emailsService = emailsService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(Input.Email);

                if (user == null)
                {
                    return Page();
                }

                var newPass = Guid.NewGuid().ToString();

                var result = await emailsService
                    .SendAsync(user.Email, "New password", $"Your new password is: {newPass}");

                if (result.StatusCode.ToString() == "Accepted")
                {
                    var removePassResult = await _userManager.RemovePasswordAsync(user);
                    var addPassResult = await _userManager.AddPasswordAsync(user, newPass);

                    if (removePassResult.Succeeded && addPassResult.Succeeded)
                    {
                        TempData["passwordReset"] = true;

                        return RedirectToPage("./Login");
                    }
                }
            }
            return Page();
        }
    }
}
