using BelajarRESTApi.Application.ConfigProfile;
using BelajarRESTApi.Application.DefaultServices.ProductService;
using BelajarRESTApi.Database.Databases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Create DBContext Service
var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<SalesContext>(option => option.UseSqlServer(connectionString));

// AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ConfigurationProfile());
});
var mapper = config.CreateMapper();

// Add services to the container.
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<IProductAppService, ProductAppService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.UseMiddleware<CustomExceptionMiddleware>();
//app.UseMiddleware<CustomAuthHeaderMiddleware>();
//app.UseHttpsRedirection();
//app.UseRouting();
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("Hello enigma from middleware");
//    Console.WriteLine(context.Request.Path);
//    Console.WriteLine(context.Request.Host);
//    await next();
//    Console.WriteLine(context.Response.StatusCode);
//});

//app.UseEndpoints(endpoints =>
//{
//    // endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello Enigma!"); });
//    endpoints.MapControllers();
//});

app.Run();
