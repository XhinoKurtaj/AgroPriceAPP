using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroPrice.Data.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PointOfSaleId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WholeSaleMarket",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Address = table.Column<string>(maxLength: 5000, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholeSaleMarket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointOfSale",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Description = table.Column<string>(maxLength: 5000, nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    WholeSaleMarketId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfSale_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointOfSale_WholeSaleMarket_WholeSaleMarketId",
                        column: x => x.WholeSaleMarketId,
                        principalTable: "WholeSaleMarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Origin = table.Column<string>(maxLength: 256, nullable: false),
                    SupplyDate = table.Column<DateTime>(nullable: false),
                    PointOfSaleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_PointOfSale_PointOfSaleId",
                        column: x => x.PointOfSaleId,
                        principalTable: "PointOfSale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    CurrentPrice = table.Column<decimal>(nullable: false),
                    CurrentQuantity = table.Column<int>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
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
                name: "IX_PointOfSale_UserId",
                table: "PointOfSale",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointOfSale_WholeSaleMarketId",
                table: "PointOfSale",
                column: "WholeSaleMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PointOfSaleId",
                table: "Product",
                column: "PointOfSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_ProductId",
                table: "ProductDetail",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetail");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "PointOfSale");

            migrationBuilder.DropTable(
                name: "WholeSaleMarket");

            migrationBuilder.DropColumn(
                name: "PointOfSaleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
