using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroPrice.Data.Migrations
{
    public partial class AddedProductDetailsTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetail");

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentQuantity = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_ProductId",
                table: "ProductDetail",
                column: "ProductId");
        }
    }
}
