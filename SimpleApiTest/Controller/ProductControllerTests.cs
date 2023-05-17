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

            var responseProduct = new AddNewProductResponse()
            {
                Id = 1,
                Name = testProduct.Name,
                Description = testProduct.Description,
                Price = testProduct.Price,
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.AddProduct(It.IsAny<AddNewProductRequest>())).Returns(responseProduct);
            var controller = new ProductController(serviceMock.Object);

            //Act
            var response = await controller.AddProduct(testProduct);

            //Assert
            response.ShouldNotBeNull();
            ((ObjectResult)response).StatusCode.ShouldBeEquivalentTo((int)HttpStatusCode.OK);
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
            var controller = new ProductController(serviceMock.Object);

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
            var products = new List<GetAllProductsResponse>();
            var testProduct1 = new GetAllProductsResponse
            {
                Id = 1,
                Name = "Mouse",
                Description = "Fastest gaming mouse made",
                Price = 1337
            };
            var testProduct2 = new GetAllProductsResponse
            {
                Id = 2,
                Name = "Keyboard",
                Description = "RGB",
                Price = 123
            };

            products.Add(testProduct1);
            products.Add(testProduct2);

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.GetAllProducts()).Returns(products);
            var controller = new ProductController(serviceMock.Object);

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
            var controller = new ProductController(serviceMock.Object);

        //Act
        var response = await controller.GetAllProducts();

        //Assert
        response.ShouldNotBeNull();
            ((ObjectResult) response).StatusCode.ShouldBeEquivalentTo((int) HttpStatusCode.BadRequest);
        }
}
}
