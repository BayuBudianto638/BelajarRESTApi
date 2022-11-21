using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.Application.DefaultServices.ProductService
{
    public interface IProductAppService
    {
        void Create(CreateProductDto model);
        void Update(UpdateProductDto model);
        void Delete(int id);
        PagedResult<ProductListDto> GetAllProducts(PageInfo pageinfo);
        UpdateProductDto GetProductByCode(string code);
        PagedResult<ProductListDto> SearchProduct(string searchString, PageInfo pageinfo);
    }
}
