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

        public async Task<Product> AddProduct(AddNewProductRequest productRequest)
        {
            if (productRequest.Name == null || productRequest.Name.Length == 0)
                throw new Exception(message: "Product name is not allowed");

            if (productRequest.Price == 0)
                throw new Exception(message: "Product need a price");

            var product = Mapper.Map<Product>(productRequest);
            return await Database.AddProduct(product);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            if (!Database.GetProducts().Result.Any())
                throw new Exception(message: "You need to create a Product first");

            return await Database.GetProducts();
        }
    }
}
