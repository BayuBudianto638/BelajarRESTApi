using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.Application.DefaultServices.ProductService.Dto
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQty { get; set; }
        public int SupplierId { get; set; }
    }
}
