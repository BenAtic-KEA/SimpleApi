using Shouldly;
using SimpleApiCase.Database;
using SimpleApiCase.Entities;

namespace SimpleApiTest.Database
{
    public class SimpleDatabaseTests : IDisposable
    {
        [Fact]
        public void AddProduct_ShouldBe_Success()
        {
            //Arrange
            var database = new SimpleDatabase();
            var testProduct = new Product
            {
                Name = "Mouse",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };
            //Act
            var resultBeforeAdd = database.GetProducts().Result.Count();
            var product = database.AddProduct(testProduct);
            var resultAfterAdd = database.GetProducts().Result.Count();

            //Assert
            resultBeforeAdd.ShouldBeEquivalentTo(0);
            resultAfterAdd.ShouldBeEquivalentTo(1);
            product.Result.Id.ShouldBeGreaterThan(0);
        }
        [Fact]
        public void AddProduct_WithInvalidProduct_ShouldReturn_Exception()
        {
            //Arrange
            var database = new SimpleDatabase();
            var invalidProduct = new Product { };

            //Act & Assert
            Should.Throw<Exception>(() => database.AddProduct(invalidProduct));
        }


        [Fact]
        public void AddProduct_ResponseShouldBe_EqualToRequestWithId()
        {

            //Arrange
            var database = new SimpleDatabase();
            var testProduct1 = new Product
            {
                Name = "Mouse",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };
            var testProduct2 = new Product
            {
                Name = "Keyboard",
                Description = "Flashy RGB",
                Price = 250
            };

            //Act
            var testProduct1Result = database.AddProduct(testProduct1);
            var testProduct2Result = database.AddProduct(testProduct2);

            //Assert

            testProduct1.Price.ShouldBeEquivalentTo(testProduct1Result.Result.Price);
            testProduct2.Price.ShouldBeEquivalentTo(testProduct2Result.Result.Price);
            testProduct1.Description.ShouldBeEquivalentTo(testProduct1Result.Result.Description);
            testProduct2.Description.ShouldBeEquivalentTo(testProduct2Result.Result.Description);
            testProduct1.Name.ShouldBeEquivalentTo(testProduct1Result.Result.Name);
            testProduct2.Name.ShouldBeEquivalentTo(testProduct2Result.Result.Name);
            testProduct1Result.Result.Id.ShouldBeGreaterThan(0);
            testProduct2Result.Result.Id.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void GetAllProducts_ShouldReturn_ProductList()
        {
            //Arrange
            var database = new SimpleDatabase();
            var testProduct1 = new Product
            {
                Name = "Mouse",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };
            var testProduct2 = new Product
            {
                Name = "Keyboard",
                Description = "Flashy RGB",
                Price = 250
            };

            //Act
            var testProduct1Result = database.AddProduct(testProduct1);
            var testProduct2Result = database.AddProduct(testProduct2);

            var response = database.GetProducts();

            //Assert
            response.Result.Count().ShouldBeEquivalentTo(2);
            response.Result.ToList().Exists(p => p.Id == testProduct1Result.Result.Id).ShouldBeTrue();
            response.Result.ToList().Exists(p => p.Id == testProduct2Result.Result.Id).ShouldBeTrue();
        }

        public void Dispose()
        {
            SimpleDatabase.ProductDB.Clear();
        }
    }
}