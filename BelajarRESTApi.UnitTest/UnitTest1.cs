namespace BelajarRESTApi.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
        
        using Xunit;
using Moq;

namespace MyProject.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetAllProducts_ReturnsCorrectResult()
        {
            // Arrange
            var mockContext = new Mock<ISalesContext>();
            var products = new List<Product>
            {
                new Product { ProductCode = "P1", ProductName = "Product 1", ProductPrice = 100, ProductQty = 10, SupplierId = 1 },
                new Product { ProductCode = "P2", ProductName = "Product 2", ProductPrice = 200, ProductQty = 20, SupplierId = 2 },
                new Product { ProductCode = "P3", ProductName = "Product 3", ProductPrice = 300, ProductQty = 30, SupplierId = 3 }
            };
            var suppliers = new List<Supplier>
            {
                new Supplier { SupplierId = 1, SupplierName = "Supplier 1" },
                new Supplier { SupplierId = 2, SupplierName = "Supplier 2" },
                new Supplier { SupplierId = 3, SupplierName = "Supplier 3" }
            };
            mockContext.Setup(c => c.Products).Returns(products.AsQueryable());
            mockContext.Setup(c => c.Suppliers).Returns(suppliers.AsQueryable());
            var productService = new ProductService(mockContext.Object);

            // Act
            var result = productService.GetAllProducts(new PageInfo { Skip = 0, PageSize = 2 });

            // Assert
            Assert.Equal(3, result.Total);
            Assert.Equal(2, result.Data.Count());
            Assert.Equal("Product 1", result.Data.First().ProductName);
            Assert.Equal("Supplier 1", result.Data.First().SupplierName);
            Assert.Equal("Product 2", result.Data.Last().ProductName);
            Assert.Equal("Supplier 2", result.Data.Last().SupplierName);
        }
    }
}

    }
}
