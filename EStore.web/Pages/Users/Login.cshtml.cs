using EStore.web.Models.Notifications;
using EStore.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EStore.web.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string ReturnUrl)
        {
            var signInResult = await signInManager.PasswordSignInAsync(LoginViewModel.Username, LoginViewModel.Password, false, false);

            if (signInResult.Succeeded)
            {
                return Redirect($"~{ReturnUrl}");
            }

            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong. Check your username and password!",
                Type = NotificationType.Error
            };
            return Page();

        }
    }
}
