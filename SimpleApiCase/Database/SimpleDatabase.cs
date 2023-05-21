using SimpleApiCase.Entities;
using System.Collections.Generic;

namespace SimpleApiCase.Database
{
    public sealed class SimpleDatabase : ISimpleDatabase
    {
        public static SimpleDatabase Instance { get; } = new SimpleDatabase();
        public static List<Product> ProductDB { get; } = new List<Product>();
        private static int ProductIDIncrement { get; set; } = 1;
        public SimpleDatabase() { }

        public async Task<Product> AddProduct(Product product)
        {
            if (product is null || product.Name is null || product.Description is null)
                throw new Exception();

            product.Id = ProductIDIncrement++;
            ProductDB.Add(product);
            return await Task.FromResult(product);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await Task.FromResult(ProductDB);
        }
    }
}
