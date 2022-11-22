using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using BelajarRESTApi.Application.Model;
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
        public IActionResult GetAllProduct([FromQuery] PageInfo pageinfo)
        {
            //// FromBody tidak bisa di gunakan untuk method HttpGet
            //// Ada 2 cara untuk bisa mengirim parameter ke HttpGet
            //// 1. Deklarasi variable 1 per 1
            //// 2. Gunakan FormQuery
            try
            {
                var productList = _productAppService.GetAllProducts(pageinfo);
                return Requests.Response(this, new ApiStatus(200), productList, "");
            }
            catch(Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message); // not found
            }
        }

        [HttpPost("SaveProduct")]
        public IActionResult SaveProduct([FromBody] CreateProductDto model)
        {
            try
            {
                var (isAdded, isMessage) = _productAppService.Create(model);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }

                return Requests.Response(this, new ApiStatus(200), isMessage, "Success");
            }
            catch(Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpGet("GetProductByCode")]
        public IActionResult GetProductByCode(string code)
        {
            try
            {
                var data = _productAppService.GetProductByCode(code);
                return Requests.Response(this, new ApiStatus(200), data, "");
            }
            catch(Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message); // not found
            }            
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int Id)
        {
            try
            {
                var (isDeleted, isMessage) = _productAppService.Delete(Id);
                if (!isDeleted)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }

                return Requests.Response(this, new ApiStatus(200), isMessage, "Success");
            }
            catch(Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPatch("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] UpdateProductDto model)
        {
            try
            {
                var (isUpdated, isMessage) = _productAppService.Update(model);
                if (!isUpdated)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }

                return Requests.Response(this, new ApiStatus(200), isMessage, "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpGet("SearchProduct")]
        public IActionResult SearchProduct(string searchString, [FromQuery] PageInfo pageInfo)
        {
            try
            {
                var data = _productAppService.SearchProduct(searchString, pageInfo);
                return Requests.Response(this, new ApiStatus(200), data, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message); // not found
            }
        }
    }
}
