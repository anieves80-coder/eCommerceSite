using EStore.web.Models.Notifications;
using EStore.web.Models.Domain;
using EStore.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Azure;

namespace EStore.web.Pages.Products
{
    public class ProductModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public ProductsModel product { get; set; }


        [BindProperty]
        public int addProductQty { get; set; } = 1;

        private readonly IProductsRepository productsRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ProductModel(IProductsRepository productsRepository, IShoppingCartRepository shoppingCartRepository, UserManager<IdentityUser> userManager)
        {
            this.productsRepository = productsRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var notificationJson = (string)TempData["Notification"];
            if(notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            product = await productsRepository.GetOneAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostCart(Guid id)
        {
            var userId = new Guid(userManager.GetUserId(User));

            var product = new ShoppingCartModel
            {
                UserId = userId,
                Quantity = addProductQty,
                ProductId = id
            };

            await shoppingCartRepository.AddProductAsync(product);

            var notification = new Notification
            {
                Message = "Item(s) has been added to the cart!",
                Type = NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToPage("Product", "OnGet");
        }
    }
    
}
