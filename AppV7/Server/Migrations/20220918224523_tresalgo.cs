using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppV7.Server.Migrations
{
    public partial class tresalgo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Captura",
                table: "WebSite",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Web",
                table: "WebSite",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Captura",
                table: "WebSite");

            migrationBuilder.DropColumn(
                name: "Web",
                table: "WebSite");
        }
    }
}
