using AutoMapper;
using Moq;
using Shouldly;
using SimpleApiCase.Database;
using SimpleApiCase.Entities;
using SimpleApiCase.Services;

namespace SimpleApiTest.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public void AddProduct_ShouldBe_Success()
        {
            //Arrange

            var testProduct = new AddNewProductRequest
            {
                Name = "Mouse",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };

            var responseProduct = new Product()
            {
                Id = 1,
                Name = testProduct.Name,
                Description = testProduct.Description,
                Price = testProduct.Price,
            };
            var databaseMock = new Mock<ISimpleDatabase>();
            databaseMock.Setup(p => p.AddProduct(It.IsAny<Product>())).Returns(responseProduct);
            var myProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            var productService = new ProductService(databaseMock.Object, mapper);
            //Act

            var result = productService.AddProduct(testProduct);

            //Assert
            result.ShouldBeOfType<AddNewProductResponse>();

        }

        [Fact]
        public void AddProduct_ShouldThrow_Exception()
        {
            //Arrange
            var testProduct1 = new AddNewProductRequest
            {
                Name = "",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };
            var testProduct2 = new AddNewProductRequest
            {
                Name = "Mouse",
                Description = "Fastest gaming mouse made"
            };
            var databaseMock = new Mock<ISimpleDatabase>();
            var myProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            var productService = new ProductService(databaseMock.Object, mapper);

            //Act & Assert

            Should.Throw<Exception>(() => productService.AddProduct(testProduct1));
            Should.Throw<Exception>(() => productService.AddProduct(testProduct2));

        }
        [Fact]
        public void GetAllProducts_ShouldReturn_Succes()
        {
            //Arrange
            var products = new List<Product>();
            var testProduct1 = new Product
            {
                Name = "Mouse",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };
            var testProduct2 = new Product
            {
                Name = "Keyboard",
                Description = "RGB",
                Price = 123
            };
            products.Add(testProduct1);
            products.Add(testProduct2);
            var databaseMock = new Mock<ISimpleDatabase>();
            databaseMock.Setup(p => p.GetProducts()).Returns(products);
            var myProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            var productService = new ProductService(databaseMock.Object, mapper);

            //Act
            var result = productService.GetAllProducts();

            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }
        [Fact]
        public void GetAllProducts_ShouldThrow_Exception()
        {
            //Arrange
            var databaseMock = new Mock<ISimpleDatabase>();
            databaseMock.Setup(p => p.GetProducts()).Throws<Exception>();
            var myProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            var productService = new ProductService(databaseMock.Object, mapper);

            //Act & Assert
            Should.Throw<Exception>(() => productService.GetAllProducts());

        }
    }
}
