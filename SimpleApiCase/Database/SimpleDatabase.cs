using SimpleApiCase.Entities;

namespace SimpleApiCase.Database
{
    public sealed class SimpleDatabase : ISimpleDatabase
    {
        public static SimpleDatabase Instance { get; } = new SimpleDatabase();
        public List<Product> ProductDB { get; } = new List<Product>();
        private int ProductIDIncrement { get; set; } = 1;
        public SimpleDatabase() { }

        //public static SimpleDatabase GetInstance()
        //{
        //    return Instance;
        //}

        public Product AddProduct(Product product)
        {
            if (product is null || product.Name is null || product.Description is null)
                throw new Exception();

            product.Id = ProductIDIncrement++;
            ProductDB.Add(product);
            return product;
        }

        public List<Product> GetProducts()
        {
            return ProductDB;
        }
    }
}
