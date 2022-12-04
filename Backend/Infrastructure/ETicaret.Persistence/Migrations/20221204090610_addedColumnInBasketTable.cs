using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    public partial class addedColumnInBasketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BasketId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Baskets");

            migrationBuilder.AddColumn<int>(
                name: "BasketStatus",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketStatus",
                table: "Baskets");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Baskets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId",
                unique: true,
                filter: "[BasketId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");
        }
    }
}
