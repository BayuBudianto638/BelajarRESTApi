using BelajarRESTApi.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.Database.Databases
{
    public class SalesContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        public SalesContext(DbContextOptions<SalesContext> options): base(options)
        {

        }
    }
}
