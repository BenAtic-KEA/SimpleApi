using SimpleApiCase.Entities;

namespace SimpleApiCase.Database
{
    public interface ISimpleDatabase
    {
        public Task<Product> AddProduct(Product product);

        public Task<IEnumerable<Product>> GetProducts();
    }
}
