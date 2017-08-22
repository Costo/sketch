using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SketchCore.Web.Migrations
{
    public partial class AddStockPhotoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImportedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PublishedDate = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UniqueId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OEmbedInfo_AuthorName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_AuthorUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_Height = table.Column<int>(type: "int", nullable: false),
                    OEmbedInfo_ProviderName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_ProviderUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_ThumbnailHeight = table.Column<int>(type: "int", nullable: false),
                    OEmbedInfo_ThumbnailUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_ThumbnailWidth = table.Column<int>(type: "int", nullable: false),
                    OEmbedInfo_Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_Type = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_Version = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OEmbedInfo_Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPhoto", x => x.Id);
                    table.UniqueConstraint("AK_StockPhoto_UniqueId", x => x.UniqueId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockPhoto");
        }
    }
}
