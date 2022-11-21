using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BelajarRESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet("GetAllProduct")]
        [Produces("application/json")]
        public PagedResult<ProductListDto> GetAllProduct([FromQuery] PageInfo pageinfo)
        {
            // FromBody tidak bisa di gunakan untuk method HttpGet
            // Ada 2 cara untuk bisa mengirim parameter ke HttpGet
            // 1. Deklarasi variable 1 per 1
            // 2. Gunakan FormQuery
            var productList = _productAppService.GetAllProducts(pageinfo);

            return productList;
        }

        [HttpPost("SaveProduct")]
        public void SaveProduct([FromBody] CreateProductDto model)
        {
            _productAppService.Create(model);
        }

        [HttpGet("GetProductByCode")]
        public UpdateProductDto GetProductByCode(string code)
        {
            var data = _productAppService.GetProductByCode(code);
            return data;
        }

        [HttpDelete("DeleteProduct")]
        public void DeleteProduct(int Id)
        {
            _productAppService.Delete(Id);
        }

        [HttpPatch("UpdateProduct")]
        public void UpdateProduct([FromBody] UpdateProductDto model)
        {
            _productAppService.Update(model);
        }

        [HttpGet("SearchProduct")]
        public PagedResult<ProductListDto> SearchProduct(string searchString, [FromQuery] PageInfo pageInfo)
        {
            var data = _productAppService.SearchProduct(searchString, pageInfo);
            return data;
        }
    }
}
