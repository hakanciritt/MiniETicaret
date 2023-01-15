using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    public partial class removedColumnsFromMetaContentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextContents_MetaContents_MetaContentId",
                table: "TextContents");

            migrationBuilder.DropColumn(
                name: "TextContentId",
                table: "MetaContents");

            migrationBuilder.AlterColumn<Guid>(
                name: "MetaContentId",
                table: "TextContents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TextContents_MetaContents_MetaContentId",
                table: "TextContents",
                column: "MetaContentId",
                principalTable: "MetaContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextContents_MetaContents_MetaContentId",
                table: "TextContents");

            migrationBuilder.AlterColumn<Guid>(
                name: "MetaContentId",
                table: "TextContents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "TextContentId",
                table: "MetaContents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TextContents_MetaContents_MetaContentId",
                table: "TextContents",
                column: "MetaContentId",
                principalTable: "MetaContents",
                principalColumn: "Id");
        }
    }
}
