using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using SimpleApiCase.Controllers;
using SimpleApiCase.Entities;
using SimpleApiCase.Services;
using System.Net;

namespace SimpleApiTest.Controller
{
    public class ProductControllerTests
    {


        [Fact]
        public async Task AddProduct_ShouldReturn_Success()
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
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.AddProduct(It.IsAny<AddNewProductRequest>())).Returns(Task.FromResult(responseProduct));
            IMapper mapper = AddAutomapper();
            var controller = new ProductController(serviceMock.Object,mapper);

            //Act
            var response = await controller.AddProduct(testProduct);
            //Assert
            response.ShouldNotBeNull();
        }
        [Fact]
        public async Task AddProduct_ShouldReturn_BadRequest()
        {
            //Arrange
            var testProduct = new AddNewProductRequest
            {
                Name = "",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.AddProduct(It.IsAny<AddNewProductRequest>())).Throws<Exception>();
            IMapper mapper = AddAutomapper();
            var controller = new ProductController(serviceMock.Object,mapper);

            //Act
            var response = await controller.AddProduct(testProduct);

            //Assert
            response.ShouldNotBeNull();
            ((ObjectResult)response).StatusCode.ShouldBeEquivalentTo((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturn_Success()
        {

            //Arrange
            var products = new List<Product>();
            var testProduct1 = new Product
            {
                Id = 1,
                Name = "Mouse",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };
            var testProduct2 = new Product
            {
                Id = 2,
                Name = "Keyboard",
                Description = "RGB",
                Price = 123
            };

            products.Add(testProduct1);
            products.Add(testProduct2);

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.GetAllProducts()).Returns(() => Task.FromResult(products.AsEnumerable()));
            IMapper mapper = AddAutomapper();
            var controller = new ProductController(serviceMock.Object, mapper);

            //Act
            var response = await controller.GetAllProducts();

            //Assert

            response.ShouldNotBeNull();
            ((ObjectResult)response).StatusCode.ShouldBeEquivalentTo((int)HttpStatusCode.OK);

        }
        [Fact]
        public async Task GetAllProducts_ShouldThrow_Exception()
        {
            //Arrange

        var serviceMock = new Mock<IProductService>();
        serviceMock.Setup(s => s.GetAllProducts()).Throws<Exception>();
            IMapper mapper = AddAutomapper();
            var controller = new ProductController(serviceMock.Object, mapper);

        //Act
        var response = await controller.GetAllProducts();

        //Assert
        response.ShouldNotBeNull();
            ((ObjectResult) response).StatusCode.ShouldBeEquivalentTo((int) HttpStatusCode.BadRequest);
        }

        public IMapper AddAutomapper()
        {
            var myProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            return new Mapper(configuration);
        }
}
}
