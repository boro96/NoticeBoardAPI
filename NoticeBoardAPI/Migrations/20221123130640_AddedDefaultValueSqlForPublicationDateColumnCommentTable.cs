using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoticeBoardAPI.Migrations
{
    public partial class AddedDefaultValueSqlForPublicationDateColumnCommentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Comments",
                type: "datetime2(3)",
                precision: 3,
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Comments",
                type: "datetime2(3)",
                precision: 3,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3,
                oldNullable: true,
                oldDefaultValueSql: "getdate()");
        }
    }
}
