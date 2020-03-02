using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CarWashPOI.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarWashPOI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(11, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var adminUsername = _configuration["AdminUserCredentials:Username"];
            var adminEmail = _configuration["AdminUserCredentials:Email"];

            var userWithSuchEmail = await _userManager.FindByEmailAsync(Input.Email);

            if (Input.Username.ToUpper() == adminUsername.ToUpper()
                && Input.Email.ToUpper() != adminEmail.ToUpper())
            {
                ModelState.AddModelError(nameof(Input.Username), "Please try with different username.");
            }
            else if ((Input.Email.ToUpper() == adminEmail.ToUpper() && Input.Username.ToUpper() != adminUsername.ToUpper())
                || userWithSuchEmail != null)
            {
                ModelState.AddModelError(nameof(Input.Email), "Please try with different e-mail address.");
            }
            else if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Username, Email = Input.Email };

                if (user.UserName.ToUpper() == adminUsername.ToUpper()
                    && user.Email.ToUpper() == adminEmail.ToUpper())
                {
                    await this.CreateAdminRoleAndFirstAdminUser();
                    return Redirect("/Identity/Account/Login");
                }
                else
                {
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        private async Task CreateAdminRoleAndFirstAdminUser()
        {
            var adminRole = new IdentityRole("Admin");
            await _roleManager.CreateAsync(adminRole);

            var firstAdminUser = new ApplicationUser
            {
                UserName = _configuration["AdminUserCredentials:Username"],
                Email = _configuration["AdminUserCredentials:Email"]
            };
            await _userManager.CreateAsync(firstAdminUser, _configuration["AdminUserCredentials:Password"]);

            await _userManager.AddToRoleAsync(firstAdminUser, "Admin");
        }
    }
}
