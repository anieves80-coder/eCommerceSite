using EStore.web.Models.Domain;
using EStore.web.Models.ViewModels;
using EStore.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.web.Pages
{
    [Authorize(Roles = "User")]
    public class AddModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public AddProductViewModel Product { get; set; }

        private IProductsRepository productRepsitory;

        public AddModel(IProductsRepository productRepsitory, UserManager<IdentityUser> userManager) 
        {
            this.productRepsitory = productRepsitory;
            this.userManager = userManager;
        }
                
        public async Task<IActionResult> OnPostAsync()
        {
            if (Product != null && ModelState.IsValid)
            {
                var userId =  new Guid(userManager.GetUserId(User));
                var product = new ProductsModel
                {
                    Title = Product.Title,
                    Description = Product.Description,
                    PicURL = Product.PicURL,
                    Price = Product.Price,
                    PostedDate = DateTime.Now,
                    Category = Product.Category.ToUpper(),
                    Owner = userId                
                };                
                await productRepsitory.AddProductAsync(product);
                return RedirectToPage("/Products/List");
            }
             return Page();                       
        }
    }
}

