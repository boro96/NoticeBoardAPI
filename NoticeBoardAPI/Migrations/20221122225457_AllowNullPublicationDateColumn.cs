using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoticeBoardAPI.Migrations
{
    public partial class AllowNullPublicationDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Comments",
                type: "datetime2(3)",
                precision: 3,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Comments",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3,
                oldNullable: true);
        }
    }
}
