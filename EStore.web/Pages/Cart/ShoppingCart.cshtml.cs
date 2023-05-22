using EStore.web.Models.ViewModels;
using EStore.web.Models.Domain;
using EStore.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EStore.web.Models.Notifications;
using System.Text.Json;

namespace EStore.web.Pages.Cart
{
    
    public class ShoppingCartModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IProductsRepository productsRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public List<CartProductViewModel> listCart = new List<CartProductViewModel>();
        private double price { get; set; } = 0.00;
        public string? totalPrice { get; set; }

        [BindProperty]
        public int cartQty { get; set; } = 1;

        [BindProperty]
        public Guid cartId { get; set; }

        public ShoppingCartModel(IProductsRepository productsRepository, IShoppingCartRepository shoppingCartRepository, UserManager<IdentityUser> userManager)
        {
            this.productsRepository = productsRepository;
            this.userManager = userManager;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = userManager.GetUserId(User);

            if (user != null)
            {
                var userId = new Guid(user);               
                var inCart = await shoppingCartRepository.GetAllByOwnerAsync(userId);
                foreach (var item in inCart)
                {
                    var oneItem = new CartProductViewModel
                    {
                        Product = await productsRepository.GetOneAsync(item.ProductId),
                        CartItemId = item.Id,
                        CartItemQty = item.Quantity
                    };
                    listCart.Add(oneItem);
                    price += oneItem.Product.Price * item.Quantity;
                }               
            }
            totalPrice = price.ToString("F");

            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            return Page();
        }
        public async Task<IActionResult> OnPostRemove(Guid Id)
        {            
            var res = await shoppingCartRepository.DeleteAsync(Id);
            if(res == null)
            {
                var notification = new Notification
                {
                    Message = "Something went wrong. Try again or contact the administrator!",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
            } else
            {
                var notification = new Notification
                {
                    Message = "Item was removed from the cart!",
                    Type = NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
            }
            return RedirectToPage("ShoppingCart", "OnGet");
        }

        public async Task<IActionResult> OnPostUpdate()
        {   
            var res = await shoppingCartRepository.EditProductAsync(cartId, cartQty);
            if(res != null)
            {
                var notification = new Notification
                {
                    Message = "Item quantity was updated!",
                    Type = NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
            }
            return RedirectToPage("ShoppingCart", "OnGet");
        }

    }
}

