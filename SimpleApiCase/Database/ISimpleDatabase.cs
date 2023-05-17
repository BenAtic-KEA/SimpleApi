using SimpleApiCase.Entities;

namespace SimpleApiCase.Database
{
    public interface ISimpleDatabase
    {
        public Product AddProduct(Product product);

        public List<Product> GetProducts();
    }
}
