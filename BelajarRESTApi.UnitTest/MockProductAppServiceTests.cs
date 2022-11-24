using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.UnitTest
{
    public class MockProductAppServiceTests : Mock<IProductAppService>
    {
        public MockProductAppServiceTests GetAllProduct()
        {
            PageInfo pageInfo = new PageInfo();
            pageInfo.Page = 1;
            pageInfo.PageSize = 5;

            PagedResult<ProductListDto> pagedResult = new PagedResult<ProductListDto>();

           Setup(x => x.GetAllProducts(pageInfo))
                .ReturnsAsync(pagedResult);

            return this;
        }

        public MockProductAppServiceTests CreateProduct()
        {
            CreateProductDto createProductDto = new CreateProductDto();
            createProductDto.ProductCode = "PRD-001";
            createProductDto.ProductName = "TEST";
            createProductDto.ProductPrice = 1000;
            createProductDto.ProductQty = 1;
            createProductDto.SupplierId = 1;

            Setup(x => x.Create(createProductDto));

            return this;
        }
    }
}
