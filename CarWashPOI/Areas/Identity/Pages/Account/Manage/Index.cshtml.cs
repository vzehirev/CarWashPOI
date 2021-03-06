﻿using CarWashPOI.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CarWashPOI.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Потребителско име")]
            public string Username { get; set; }

            [Required]
            [StringLength(11, ErrorMessage = "{0} трябва да е с дължина между {2} и {1} символа.", MinimumLength = 3)]
            [Display(Name = "Ново потребителско име")]
            public string NewUsername { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            string currentUsername = await _userManager.GetUserNameAsync(user);

            Input = new InputModel
            {
                Username = currentUsername
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ApplicationUser userWithSuchUsername = await _userManager.FindByNameAsync(Input.NewUsername);
            if (userWithSuchUsername != null)
            {
                ModelState.AddModelError("NewUsername", "Моля, опитайте с друго потребителско име.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            string userName = await _userManager.GetUserNameAsync(user);
            if (Input.NewUsername != userName)
            {
                IdentityResult setUsernameResult = await _userManager.SetUserNameAsync(user, Input.NewUsername);
                if (!setUsernameResult.Succeeded)
                {
                    string userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting username for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Вашият профил беше обновен";
            return RedirectToPage();
        }
    }
}
