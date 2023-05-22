using EStore.web.Models.Domain;

namespace EStore.web.Models.ViewModels
{
    public class CartProductViewModel
    {
        public Guid Id { get; set; }        
        public ProductsModel Product { get; set; }        
        public Guid CartItemId { get; set; }

        public int CartItemQty { get; set; }

    }
}
