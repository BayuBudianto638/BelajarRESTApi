using BelajarRESTApi.Application.ConfigProfile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Database.Databases;

namespace BelajarRESTApi.UnitTest
{
    public class Startup
    {
        public Startup()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<SalesContext>(option =>
                    option.UseInMemoryDatabase("Server=FAIRUZ-PC\\SQLEXPRESS;Database=SalesDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ConfigurationProfile());
            });
            var mapper = config.CreateMapper();

            // Add services to the container.
            serviceCollection.AddSingleton(mapper);
            serviceCollection.AddTransient<IProductAppService, ProductAppService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }   
}
