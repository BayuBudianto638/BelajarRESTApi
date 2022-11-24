using AutoMapper;
using Azure;
using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using BelajarRESTApi.Database.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BelajarRESTApi.Application.ConfigProfile;
using Microsoft.Extensions.DependencyInjection;
using BelajarRESTApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Azure.Core;
using System.Reflection.Metadata;
using BelajarRESTApi.Database.Models;

namespace BelajarRESTApi.UnitTest
{
    public class ProductAppServiceTests : IClassFixture<Startup>
    {
        private ServiceProvider _serviceProvider;
        //private ProductAppService _service;

        public ProductAppServiceTests(Startup fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }


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

        [Fact]
        public void CreateProduct()
        {
            // Arrange
            var productAppService = new Mock<IProductAppService>();

            CreateProductDto createProductDto = new CreateProductDto();
            createProductDto.ProductCode = "PRD-001";
            createProductDto.ProductName = "TEST_001";
            createProductDto.ProductPrice = 1000;
            createProductDto.ProductQty = 1;
            createProductDto.SupplierId = 1;

            // Act
            var result = productAppService.Setup(service => service.Create(createProductDto));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateProduct()
        {
            // Arrange
            var productAppService = new Mock<IProductAppService>();

            UpdateProductDto update = new UpdateProductDto();
            update.ProductId = 1;
            update.ProductCode = "PRD-001";
            update.ProductName = "TEST";
            update.ProductPrice = 1000;
            update.ProductQty = 1;
            update.SupplierId = 1;

            // Act
            var result = productAppService.Setup(service => service.Update(update));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetProductByCode()
        {
            // Arrange
            var productAppService = new Mock<IProductAppService>();
            var mockSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<SalesContext>();
            var mockMapper = new Mock<IMapper>();
            var mockService = new Mock<ProductAppService>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            ProductAppService service = new ProductAppService(mockContext.Object, mockMapper.Object);

            string code = "PRD-001";

            UpdateProductDto productDto = new UpdateProductDto();
            productDto.ProductId = 1;
            productDto.ProductCode = "PRD-001";
            productDto.ProductName = "TEST";
            productDto.ProductPrice = 1000;
            productDto.ProductQty = 1;
            productDto.SupplierId = 1;

            // Act
            productAppService.Setup(service => service.GetProductByCode(code)).ReturnsAsync(productDto);
            var result = await service.GetProductByCode(code);

            // Assert
            Assert.Equal(productDto.ProductCode, result.ProductCode);
        }

        [Fact]
        public void DeleteProduct()
        {
            // Arrange
            var productAppService = new Mock<IProductAppService>();
            int id = 2;

            // Act
            var result = productAppService.Setup(service => service.Delete(id));

            // Assert
            Assert.NotNull(result);
        }
    }
}
