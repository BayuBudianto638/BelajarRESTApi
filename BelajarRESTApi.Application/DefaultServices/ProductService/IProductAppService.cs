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
        (bool, string) Create(CreateProductDto model);
        (bool, string) Update(UpdateProductDto model);
        (bool, string) Delete(int id);
        PagedResult<ProductListDto> GetAllProducts(PageInfo pageinfo);
        UpdateProductDto GetProductByCode(string code);
        PagedResult<ProductListDto> SearchProduct(string searchString, PageInfo pageinfo);
    }
}
