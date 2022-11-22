using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.Application.DefaultServices.ProductService
{
    public interface IProductAppService
    {
        Task<(bool, string)> Create(CreateProductDto model);
        Task<(bool, string)> Update(UpdateProductDto model);
        Task<(bool, string)> Delete(int id);
        Task<PagedResult<ProductListDto>> GetAllProducts(PageInfo pageinfo);
        Task<UpdateProductDto> GetProductByCode(string code);
        Task<PagedResult<ProductListDto>> SearchProduct(string searchString, PageInfo pageinfo);
    }
}
