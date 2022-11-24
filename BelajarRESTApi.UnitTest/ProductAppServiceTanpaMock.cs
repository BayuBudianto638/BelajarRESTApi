using BelajarRESTApi.Application.ConfigProfile;
using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using BelajarRESTApi.Database.Databases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.UnitTest
{
    //public class ServiceFixture
    //{
    //    public ServiceFixture()
    //    {
    //        var serviceCollection = new ServiceCollection();
            
    //        serviceCollection.AddDbContext<SalesContext>(option => 
    //            option.UseSqlServer("Server=FAIRUZ-PC\\SQLEXPRESS;Database=SalesDB;Trusted_Connection=True;TrustServerCertificate=True;"));

    //        var config = new AutoMapper.MapperConfiguration(cfg =>
    //        {
    //            cfg.AddProfile(new ConfigurationProfile());
    //        });
    //        var mapper = config.CreateMapper();

    //        // Add services to the container.
    //        serviceCollection.AddSingleton(mapper);

    //        serviceCollection.AddTransient<IProductAppService, ProductAppService>();

    //        ServiceProvider = serviceCollection.BuildServiceProvider();
    //    }

    //    public ServiceProvider ServiceProvider { get; private set; }
    //}

    public class ProductAppServiceTanpaMock : IClassFixture<Startup>
    {
        private ServiceProvider _serviceProvider;

        public ProductAppServiceTanpaMock(Startup fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void CreateProduct()
        {
            var service = _serviceProvider.GetService<IProductAppService>();

            CreateProductDto createProductDto = new CreateProductDto();
            createProductDto.ProductCode = "PRD-001";
            createProductDto.ProductName = "TEST";
            createProductDto.ProductPrice = 1000;
            createProductDto.ProductQty = 1;
            createProductDto.SupplierId = 1;

            var result = service.Create(createProductDto);

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateProduct()
        {
            var service = _serviceProvider.GetService<IProductAppService>();

            UpdateProductDto updateProductDto = new UpdateProductDto();
            updateProductDto.ProductId = 2;
            updateProductDto.ProductCode = "PRD-001";
            updateProductDto.ProductName = "TEST";
            updateProductDto.ProductPrice = 1000;
            updateProductDto.ProductQty = 1;
            updateProductDto.SupplierId = 1;

            var result = service.Update(updateProductDto);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllProduct()
        {
            var service = _serviceProvider.GetService<IProductAppService>();

            PageInfo pageInfo = new PageInfo();
            pageInfo.Page = 1;
            pageInfo.PageSize = 5;

            var result = service.GetAllProducts(pageInfo);

            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteProduct()
        {
            var service = _serviceProvider.GetService<IProductAppService>();

            int id = 6;

            var result = await service.Delete(id);

            Assert.True(result.Item1);
        }
    }
}
