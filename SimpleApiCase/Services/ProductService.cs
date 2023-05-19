using AutoMapper;
using SimpleApiCase.Database;
using SimpleApiCase.Entities;

namespace SimpleApiCase.Services
{
    public class ProductService : IProductService
    {
        private readonly ISimpleDatabase Database;
        private readonly IMapper Mapper;

        public ProductService(ISimpleDatabase database, IMapper mapper)
        {
            Database = database;
            Mapper = mapper;
        }

        public Product AddProduct(AddNewProductRequest productRequest)
        {
            if (productRequest.Name == null || productRequest.Name.Length == 0)
                throw new Exception(message: "Product name is not allowed");

            if (productRequest.Price == 0)
                throw new Exception(message: "Product need a price");

            var product = Mapper.Map<Product>(productRequest);
            var addedProduct = Database.AddProduct(product);
            
            return addedProduct;
        }

        public List<Product> GetAllProducts()
        {
            if (Database.GetProducts().Count == 0)
                throw new Exception(message: "You need to create a Product first");

            var products = Database.GetProducts();
            return products;
        }
    }
}
