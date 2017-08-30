using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SketchCore.Web.Migrations
{
    public partial class UpdateStockPhotoentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OEmbedInfo_AuthorName",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_AuthorUrl",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_Height",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_ProviderName",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_ProviderUrl",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_ThumbnailHeight",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_ThumbnailUrl",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_ThumbnailWidth",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_Type",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_Url",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_Version",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "OEmbedInfo_Width",
                table: "StockPhoto");

            migrationBuilder.RenameColumn(
                name: "OEmbedInfo_Title",
                table: "StockPhoto",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "ContentUrl",
                table: "StockPhoto",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Copyright",
                table: "StockPhoto",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StockPhoto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Thumbnail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Height = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thumbnail_StockPhoto_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "StockPhoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_PhotoId",
                table: "Thumbnail",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thumbnail");

            migrationBuilder.DropColumn(
                name: "ContentUrl",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "Copyright",
                table: "StockPhoto");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StockPhoto");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "StockPhoto",
                newName: "OEmbedInfo_Title");

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_AuthorName",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_AuthorUrl",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OEmbedInfo_Height",
                table: "StockPhoto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_ProviderName",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_ProviderUrl",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OEmbedInfo_ThumbnailHeight",
                table: "StockPhoto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_ThumbnailUrl",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OEmbedInfo_ThumbnailWidth",
                table: "StockPhoto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_Type",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_Url",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OEmbedInfo_Version",
                table: "StockPhoto",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OEmbedInfo_Width",
                table: "StockPhoto",
                nullable: false,
                defaultValue: 0);
        }
    }
}
