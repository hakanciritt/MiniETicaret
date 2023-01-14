using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    public partial class changeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_MainCategoryId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_TextContent_TextContentId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Category_CategoriesId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_TextContent_TextContentId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_TextContent_MetaContent_MetaContentId",
                table: "TextContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextContent",
                table: "TextContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaContent",
                table: "MetaContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "TextContent",
                newName: "TextContents");

            migrationBuilder.RenameTable(
                name: "MetaContent",
                newName: "MetaContents");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_TextContent_MetaContentId",
                table: "TextContents",
                newName: "IX_TextContents_MetaContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_TextContentId",
                table: "Categories",
                newName: "IX_Categories_TextContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_MainCategoryId",
                table: "Categories",
                newName: "IX_Categories_MainCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextContents",
                table: "TextContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaContents",
                table: "MetaContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_TextContents_TextContentId",
                table: "Categories",
                column: "TextContentId",
                principalTable: "TextContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoriesId",
                table: "ProductCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TextContents_TextContentId",
                table: "Products",
                column: "TextContentId",
                principalTable: "TextContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextContents_MetaContents_MetaContentId",
                table: "TextContents",
                column: "MetaContentId",
                principalTable: "MetaContents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_TextContents_TextContentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoriesId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_TextContents_TextContentId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_TextContents_MetaContents_MetaContentId",
                table: "TextContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextContents",
                table: "TextContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaContents",
                table: "MetaContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "TextContents",
                newName: "TextContent");

            migrationBuilder.RenameTable(
                name: "MetaContents",
                newName: "MetaContent");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_TextContents_MetaContentId",
                table: "TextContent",
                newName: "IX_TextContent_MetaContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_TextContentId",
                table: "Category",
                newName: "IX_Category_TextContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Category",
                newName: "IX_Category_MainCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextContent",
                table: "TextContent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaContent",
                table: "MetaContent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_MainCategoryId",
                table: "Category",
                column: "MainCategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_TextContent_TextContentId",
                table: "Category",
                column: "TextContentId",
                principalTable: "TextContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Category_CategoriesId",
                table: "ProductCategories",
                column: "CategoriesId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TextContent_TextContentId",
                table: "Products",
                column: "TextContentId",
                principalTable: "TextContent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextContent_MetaContent_MetaContentId",
                table: "TextContent",
                column: "MetaContentId",
                principalTable: "MetaContent",
                principalColumn: "Id");
        }
    }
}
