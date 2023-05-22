using EStore.web.Models.Notifications;
using EStore.web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EStore.web.Pages.Users
{
    [Authorize(Roles = "User")]
    [ValidateAntiForgeryToken]
    public class EditModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }

        public string oldPwd { get; set; }

        [BindProperty]
        public string newPwd { get; set; }

        [BindProperty]
        public string confirmNewPwd { get; set; }
        public EditModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet(string Id)
        {
            IdentityUser user = await userManager.FindByIdAsync(Id.ToString());
            if (user != null)
            {
                EditUserViewModel = new EditUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                };
            }

            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await userManager.FindByIdAsync(EditUserViewModel.Id);

            if (user != null)
            {
                user.Email = EditUserViewModel.Email;
                user.UserName = EditUserViewModel.UserName;
            }

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Information updated succesfully!",
                    Type = NotificationType.Success
                };
                return Page();
            }
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong. Try again",
                Type = NotificationType.Error
            };
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            IdentityUser user = await userManager.FindByIdAsync(EditUserViewModel.Id);

            if (user != null)
            {
                await signInManager.SignOutAsync();
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
            }
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong. Try again",
                Type = NotificationType.Error
            };
            return Page();
        }
                
        public async Task<IActionResult> OnPostUpdate()
        {

            if(String.IsNullOrEmpty(newPwd) || newPwd != confirmNewPwd)
            {
                var notification = new Notification
                {
                    Message = "Error - Passwords must match!",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
            }
            else
            {
                var user = await userManager.FindByIdAsync(EditUserViewModel.Id);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);                    
                    var result = await userManager.ResetPasswordAsync(user, token, newPwd);

                    if (result.Succeeded)
                    {
                        var notification = new Notification
                        {
                            Message = "Success - password was changed!",
                            Type = NotificationType.Success
                        };
                        TempData["Notification"] = JsonSerializer.Serialize(notification);                        
                    } 
                    else
                    {
                        var notification = new Notification
                        {
                            Message = $"Something went wrong. {result}!",
                            Type = NotificationType.Error
                        };
                        TempData["Notification"] = JsonSerializer.Serialize(notification);
                    }
                }
            }
            
            return RedirectToPage("Edit", "OnGet");
        }
    }
}