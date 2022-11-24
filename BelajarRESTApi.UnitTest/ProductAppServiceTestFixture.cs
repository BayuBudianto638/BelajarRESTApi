using BelajarRESTApi.Application.ConfigProfile;
using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Database.Databases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BelajarRESTApi.Application.Helpers;

namespace BelajarRESTApi.UnitTest
{  
    public class ProductAppServiceTestFixture
    {
        [Fact]
        public async Task GetAllProduct()
        {   
            var mockArticleRepository = new MockProductAppServiceTests()
                .GetAllProduct();

            var result = mockArticleRepository.GetAllProduct();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create()
        {
            var mockArticleRepository = new MockProductAppServiceTests()
                .GetAllProduct();

            var result = mockArticleRepository.CreateProduct();

            Assert.NotNull(result);
        }
    }
}
