using SimpleApiCase.Entities;

namespace SimpleApiCase.Services
{
    public interface IProductService
    {
        public AddNewProductResponse AddProduct(AddNewProductRequest productRequest);
        public List<GetAllProductsResponse> GetAllProducts();
    }
}