using EStore.web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public List<UsersViewModel> UsersViewModel = new List<UsersViewModel>();

        [BindProperty]
        public UserViewModel UserId { get; set; }
        
        public UsersModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            List<IdentityUser> users = userManager.Users.ToList();
            foreach (var user in users)
            {
                if (user != null)
                {
                    var userView = new UsersViewModel
                    {
                        Id = new Guid(user.Id),
                        Username = user.UserName,
                        Email = user.Email
                    };

                    UsersViewModel.Add(userView);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        { 
            IdentityUser user = await userManager.FindByIdAsync(UserId.Id.ToString());

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Admin/Users");
                }
            }
           
            return RedirectToPage("/Admin/Users"); 
        }
    }
}