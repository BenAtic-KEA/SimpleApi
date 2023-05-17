using Microsoft.AspNetCore.Mvc;
using SimpleApiCase.Entities;
using SimpleApiCase.Services;

namespace SimpleApiCase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var response = productService.GetAllProducts();

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddProduct(
            [FromBody] AddNewProductRequest productRequest
            )
        {
            try
            {
                var response = productService.AddProduct(productRequest);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}