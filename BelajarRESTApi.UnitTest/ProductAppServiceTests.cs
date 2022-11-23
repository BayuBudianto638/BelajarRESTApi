using AutoMapper;
using Azure;
using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using BelajarRESTApi.Database.Databases;
using BelajarRESTApi.UnitTest.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.UnitTest
{
    public class ProductAppServiceTests
    {

        [Fact]
        public void GetAllProducts()
        {
            // Arrange
            var productAppService = new Mock<IProductAppService>();

            PageInfo pageInfo = new PageInfo();
            pageInfo.Page = 1;
            pageInfo.PageSize = 5;

            // Act
            Task<PagedResult<ProductListDto>> pagedResult = null;
            var result = productAppService.Setup(service => service.GetAllProducts(pageInfo)).Returns(pagedResult);

            // Assert
            Assert.NotNull(result);
        }
    }
}
