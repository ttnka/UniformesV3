using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppV7.Server.Migrations
{
    public partial class Tripas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetSolicitud_Solicitudes_SolicitudesSolicitudId",
                table: "DetSolicitud");

            migrationBuilder.AlterColumn<string>(
                name: "SolicitudesSolicitudId",
                table: "DetSolicitud",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_DetSolicitud_Solicitudes_SolicitudesSolicitudId",
                table: "DetSolicitud",
                column: "SolicitudesSolicitudId",
                principalTable: "Solicitudes",
                principalColumn: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetSolicitud_Solicitudes_SolicitudesSolicitudId",
                table: "DetSolicitud");

            migrationBuilder.UpdateData(
                table: "DetSolicitud",
                keyColumn: "SolicitudesSolicitudId",
                keyValue: null,
                column: "SolicitudesSolicitudId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SolicitudesSolicitudId",
                table: "DetSolicitud",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_DetSolicitud_Solicitudes_SolicitudesSolicitudId",
                table: "DetSolicitud",
                column: "SolicitudesSolicitudId",
                principalTable: "Solicitudes",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
