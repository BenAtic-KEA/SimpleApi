using SimpleApiCase.Entities;

namespace SimpleApiCase.Services
{
    public interface IProductService
    {
        public Product AddProduct(AddNewProductRequest productRequest);
        public List<Product> GetAllProducts();
    }
}