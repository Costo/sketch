using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SketchCore.Web.Migrations
{
    public partial class AddContentWidthandContentHeighttoStockPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentHeight",
                table: "StockPhoto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentWidth",
                table: "StockPhoto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentHeight",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "ContentWidth",
                table: "StockPhoto");
        }
    }
}
