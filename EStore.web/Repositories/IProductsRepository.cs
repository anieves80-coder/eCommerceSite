using EStore.web.Models.Domain;

namespace EStore.web.Repositories
{
    public interface IProductsRepository
    {

        public Task<IEnumerable<ProductsModel>> GetAllAsync();

        public Task<IEnumerable<ProductsModel>> GetAllByOwnerAsync(Guid Id);

        public Task<ProductsModel> AddProductAsync(ProductsModel product);

        public Task<ProductsModel> EditProductAsync(ProductsModel product);

        public Task<ProductsModel?> DeleteAsync(Guid id);

        public Task<ProductsModel?> GetOneAsync(Guid id);

        public Task<IEnumerable<ProductsModel>> FindByCatAsync(string cat);

    }
}
