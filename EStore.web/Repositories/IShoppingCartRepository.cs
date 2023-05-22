using EStore.web.Models.Domain;

namespace EStore.web.Repositories
{
    public interface IShoppingCartRepository
    {        
        public Task<IEnumerable<ShoppingCartModel>> GetAllByOwnerAsync(Guid Id);

        public Task<ShoppingCartModel> AddProductAsync(ShoppingCartModel product);

        public Task<ShoppingCartModel> EditProductAsync(Guid Id, int qty);

        public Task<ShoppingCartModel?> DeleteAsync(Guid id);
        

        
    }
}
