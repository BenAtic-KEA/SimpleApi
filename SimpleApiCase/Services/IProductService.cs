using SimpleApiCase.Entities;

namespace SimpleApiCase.Services
{
    public interface IProductService
    {
        public Task<Product> AddProduct(AddNewProductRequest productRequest);
        public Task<IEnumerable<Product>> GetAllProducts();
    }
}