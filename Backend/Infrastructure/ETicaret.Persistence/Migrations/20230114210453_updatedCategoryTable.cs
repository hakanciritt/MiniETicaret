﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    public partial class updatedCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_TextContents_TextContentId",
                table: "Categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "TextContentId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_TextContents_TextContentId",
                table: "Categories",
                column: "TextContentId",
                principalTable: "TextContents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_TextContents_TextContentId",
                table: "Categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "TextContentId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_TextContents_TextContentId",
                table: "Categories",
                column: "TextContentId",
                principalTable: "TextContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
