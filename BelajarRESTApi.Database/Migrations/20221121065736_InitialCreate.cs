using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelajarRESTApi.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "dbo",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "dbo",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCode = table.Column<string>(type: "Nvarchar(10)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "Nvarchar(10)", nullable: false),
                    ProductName = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    ProductPrice = table.Column<int>(type: "int", nullable: false),
                    ProductQty = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "dbo",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetail",
                schema: "dbo",
                columns: table => new
                {
                    TransactionDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetail", x => x.TransactionDetailId);
                    table.ForeignKey(
                        name: "FK_TransactionDetail_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionDetail_Transaction_TransactionsId",
                        column: x => x.TransactionsId,
                        principalSchema: "dbo",
                        principalTable: "Transaction",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_SupplierId",
                schema: "dbo",
                table: "Product",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetail_ProductsId",
                schema: "dbo",
                table: "TransactionDetail",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetail_TransactionsId",
                schema: "dbo",
                table: "TransactionDetail",
                column: "TransactionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDetail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "dbo");
        }
    }
}
