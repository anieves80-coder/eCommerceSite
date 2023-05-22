using EStore.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.web.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public RegisterViewModel Registration { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            var user = new IdentityUser
            {
                UserName = Registration.Username, 
                Email = Registration.Email
            };
            if (Registration.Password != null)
            { 
                var res = await userManager.CreateAsync(user, Registration.Password);
                if (res.Succeeded)
                {
                    var addRolesRes = await userManager.AddToRoleAsync(user, "User");

                    if (addRolesRes.Succeeded)
                    {
                        return RedirectToPage("/Users/Login");
                    }
                }
            }
           
            return Page();
        }
    }
}

