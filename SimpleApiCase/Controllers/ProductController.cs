using AutoMapper;
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
        private readonly IMapper Mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.Mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {                
                var productsResponse = Mapper.Map<List<GetAllProductsResponse>>(await productService.GetAllProducts());
                
                return Ok(productsResponse);
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
                var productResponse = Mapper.Map<AddNewProductResponse>(await productService.AddProduct(productRequest));
                
                return Ok(productResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}