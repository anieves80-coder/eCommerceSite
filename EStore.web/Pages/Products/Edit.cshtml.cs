using EStore.web.Models.Domain;
using EStore.web.Models.Notifications;
using EStore.web.Models.ViewModels;
using EStore.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.web.Pages
{
    [Authorize(Roles = "User")]
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public AddProductViewModel Product { get; set; }
                
        public ProductsModel product { get; set; }


        private readonly IProductsRepository productsRepository;

        public EditModel(IProductsRepository productsRepository, UserManager<IdentityUser> userManager)
        {
            this.productsRepository = productsRepository;
            this.userManager = userManager;
        }
        public async Task OnGetAsync(Guid id)
        {
            product = await productsRepository.GetOneAsync(id);
        }

        public async Task<IActionResult> OnPostDelete(Guid id) 
        {
            await productsRepository.DeleteAsync(id);
            return Redirect("/Products/List");           
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            var userId = new Guid(userManager.GetUserId(User));

            if (ModelState.IsValid && userId == Product.Owner)
            {
                var dataToModify = new ProductsModel
                {
                    Id = id,
                    Title = Product.Title,
                    Description = Product.Description,
                    PicURL = Product.PicURL,
                    Price = Product.Price,
                    Category = Product.Category,
                    PostedDate = DateTime.Now
                };

                await productsRepository.EditProductAsync(dataToModify);
                return Redirect("/Products/List");
            }
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong. You are not authorized!",
                Type = NotificationType.Error
            };
            return Page();            
        }
    }
}
