using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    public partial class textContentTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "TextContentId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MetaContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextContent_MetaContent_MetaContentId",
                        column: x => x.MetaContentId,
                        principalTable: "MetaContent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_TextContentId",
                table: "Products",
                column: "TextContentId");

            migrationBuilder.CreateIndex(
                name: "IX_TextContent_MetaContentId",
                table: "TextContent",
                column: "MetaContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TextContent_TextContentId",
                table: "Products",
                column: "TextContentId",
                principalTable: "TextContent",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_TextContent_TextContentId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "TextContent");

            migrationBuilder.DropTable(
                name: "MetaContent");

            migrationBuilder.DropIndex(
                name: "IX_Products_TextContentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TextContentId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }
    }
}
