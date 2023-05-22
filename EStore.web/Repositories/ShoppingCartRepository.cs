using EStore.web.Data;
using EStore.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EStore.web.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private ProductsDbContext productsDbContext;
        public ShoppingCartRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }
        public async Task<ShoppingCartModel> AddProductAsync(ShoppingCartModel product)
        {           
            await productsDbContext.Cart.AddAsync(product);
            await productsDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<ShoppingCartModel?> DeleteAsync(Guid id)
        {
            var product = await productsDbContext.Cart.FindAsync(id);
            if (product != null)
            {
                productsDbContext.Cart.Remove(product);
                await productsDbContext.SaveChangesAsync();
            }
            return product;
        }

        public async Task<ShoppingCartModel> EditProductAsync(Guid Id, int qty)
        {
            var res = await productsDbContext.Cart.FindAsync(Id);
            if (res != null)
            {
                res.Quantity = qty;
                await productsDbContext.SaveChangesAsync();
            }
            return res;
        }

        public async Task<IEnumerable<ShoppingCartModel>> GetAllByOwnerAsync(Guid Id)
        {
            return await productsDbContext.Cart.Where(i => i.UserId == Id).ToListAsync();
        }

    }
}
