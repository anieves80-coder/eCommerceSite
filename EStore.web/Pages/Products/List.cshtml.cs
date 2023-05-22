using EStore.web.Models.Domain;
using EStore.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.web.Pages.Products
{
    [Authorize(Roles = "User")]
    public class ListModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        public List<ProductsModel> showAllProducts { get; set; }

        private readonly IProductsRepository productsRepository;

        public ListModel(IProductsRepository productsRepository, UserManager<IdentityUser> userManager)
        {
            this.productsRepository = productsRepository;
            this.userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var userId = new Guid(userManager.GetUserId(User));
            var result = await productsRepository.GetAllByOwnerAsync(userId);
            if (result != null) 
            { 
                showAllProducts = result.ToList();
            }            
        }
    }
}
