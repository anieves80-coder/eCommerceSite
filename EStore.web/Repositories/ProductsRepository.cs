using EStore.web.Data;
using EStore.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EStore.web.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductsDbContext productsDbContext;
        public ProductsRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }

        public async Task<ProductsModel> AddProductAsync(ProductsModel product)
        {
            await productsDbContext.Products.AddAsync(product);
            await productsDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<ProductsModel?> DeleteAsync(Guid id)
        {
            var product = await productsDbContext.Products.FindAsync(id);
            if (product != null)
            {
                productsDbContext.Products.Remove(product);
                await productsDbContext.SaveChangesAsync();
            }
            return product;
        }

        public async Task<ProductsModel> EditProductAsync(ProductsModel product)
        {
            var res = await productsDbContext.Products.FindAsync(product.Id);
            if (res != null)
            {
                res.Title = product.Title;
                res.Description = product.Description;
                res.PicURL = product.PicURL;
                res.Price = product.Price;
                res.PostedDate = product.PostedDate;
                res.Category = product.Category.ToUpper();
                await productsDbContext.SaveChangesAsync();
            }
            return res;
        }

        public async Task<IEnumerable<ProductsModel>> FindByCatAsync(string cat)
        {
            return await productsDbContext.Products.Where(i => i.Category == cat).ToListAsync();
        }

        public async Task<IEnumerable<ProductsModel>> GetAllAsync()
        {
            return await productsDbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<ProductsModel>> GetAllByOwnerAsync(Guid Id)
        {
            return await productsDbContext.Products.Where(i => i.Owner == Id).ToListAsync();
        }

        public async Task<ProductsModel?> GetOneAsync(Guid id)
        {
            return await productsDbContext.Products.FindAsync(id);            
            
        }
    }
}