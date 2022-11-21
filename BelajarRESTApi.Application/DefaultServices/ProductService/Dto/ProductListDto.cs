using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.Application.DefaultServices.ProductService.Dto
{
    public class ProductListDto
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQty { get; set; }
        public string SupplierName { get; set; }
    }
}
