using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.Database.Models
{
    // class model
    [Table("Product", Schema ="dbo")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Product Id")]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName="Nvarchar(10)")]
        [Display(Name ="Product Code")]
        public string ProductCode { get; set; }

        [Required]
        [Column(TypeName = "Nvarchar(100)")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public int ProductPrice { get; set; }
        public int ProductQty { get; set; }
        public int SupplierId { get; set; } // foreign key
        public virtual Supplier Supplier { get; set; } // table lain
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
