using Microsoft.EntityFrameworkCore;
using EStore.web.Models.Domain;

namespace EStore.web.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }

        public DbSet<ProductsModel> Products { get; set; }        

        public DbSet<ShoppingCartModel> Cart { get; set; }

    }
}