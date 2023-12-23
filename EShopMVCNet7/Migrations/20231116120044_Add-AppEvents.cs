using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopMVCNet7.Migrations
{
    /// <inheritdoc />
    public partial class AddAppEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppEvens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEven = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentEven = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CoverImgEven = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DiscountFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEvens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEvens");
        }
    }
}
