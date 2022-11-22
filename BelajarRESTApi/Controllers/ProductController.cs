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
        public async Task<IActionResult> GetAllProduct([FromQuery] PageInfo pageinfo)
        {
            //// FromBody tidak bisa di gunakan untuk method HttpGet
            //// Ada 2 cara untuk bisa mengirim parameter ke HttpGet
            //// 1. Deklarasi variable 1 per 1
            //// 2. Gunakan FormQuery
            try
            {
                var productList = await _productAppService.GetAllProducts(pageinfo);
                if (productList.Data.Count() < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Not Found");
                }
                return Requests.Response(this, new ApiStatus(200), productList, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message); // not found
            }
        }

        [HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProduct([FromBody] CreateProductDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _productAppService.Create(model);
                if (!isAdded)
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

        [HttpGet("GetProductByCode")]
        public async Task<IActionResult> GetProductByCode(string code)
        {
            try
            {
                var data = await _productAppService.GetProductByCode(code);
                if (data == null)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Not Found");
                }

                return Requests.Response(this, new ApiStatus(200), data, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message); // not found
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            try
            {
                var (isDeleted, isMessage) = await _productAppService.Delete(Id);
                if (!isDeleted)
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

        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto model)
        {
            try
            {
                var (isUpdated, isMessage) = await _productAppService.Update(model);
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
        public async Task<IActionResult> SearchProduct(string searchString, [FromQuery] PageInfo pageInfo)
        {
            try
            {
                var data = await _productAppService.SearchProduct(searchString, pageInfo);
                if (data.Data.Count() < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Not Found");
                }

                return Requests.Response(this, new ApiStatus(200), data, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message); // not found
            }
        }
    }
}
