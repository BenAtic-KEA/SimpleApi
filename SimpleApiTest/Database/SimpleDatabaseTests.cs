using Shouldly;
using SimpleApiCase.Database;
using SimpleApiCase.Entities;

namespace SimpleApiTest.Database
{
    public class SimpleDatabaseTests
    {
        [Fact]
        public void AddProduct_ShouldReturn_True()
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
            var resultBeforeAdd = database.ProductDB.Count;
            database.AddProduct(testProduct);
            var resultAfterAdd = database.ProductDB.Count;

            //Assert
            resultBeforeAdd.ShouldBeEquivalentTo(0);
            resultAfterAdd.ShouldBeEquivalentTo(1);
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

            testProduct1.Price.ShouldBeEquivalentTo(testProduct1Result.Price);
            testProduct2.Price.ShouldBeEquivalentTo(testProduct2Result.Price);
            testProduct1.Description.ShouldBeEquivalentTo(testProduct1Result.Description);
            testProduct2.Description.ShouldBeEquivalentTo(testProduct2Result.Description);
            testProduct1.Name.ShouldBeEquivalentTo(testProduct1Result.Name);
            testProduct2.Name.ShouldBeEquivalentTo(testProduct2Result.Name);
            testProduct1Result.Id.ShouldBeGreaterThan(0);
            testProduct2Result.Id.ShouldBeGreaterThan(0);
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
            response.Count.ShouldBeEquivalentTo(2);
            response.Exists(p => p.Id == testProduct1Result.Id).ShouldBeTrue();
            response.Exists(p => p.Id == testProduct2Result.Id).ShouldBeTrue();
        }
    }
}