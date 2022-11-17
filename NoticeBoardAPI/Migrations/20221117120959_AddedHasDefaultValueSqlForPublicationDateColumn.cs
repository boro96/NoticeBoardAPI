using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoticeBoardAPI.Migrations
{
    public partial class AddedHasDefaultValueSqlForPublicationDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Adverts",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Adverts",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3,
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
