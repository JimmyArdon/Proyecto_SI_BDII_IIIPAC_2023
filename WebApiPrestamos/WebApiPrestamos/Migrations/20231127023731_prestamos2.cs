using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiPrestamos.Migrations
{
    /// <inheritdoc />
    public partial class prestamos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "transaccional");

            migrationBuilder.RenameTable(
                name: "prestamos",
                schema: "Security",
                newName: "prestamos",
                newSchema: "transaccional");

            migrationBuilder.RenameTable(
                name: "planes_pago",
                schema: "Security",
                newName: "planes_pago",
                newSchema: "transaccional");

            migrationBuilder.RenameTable(
                name: "estados_solicitud",
                schema: "Security",
                newName: "estados_solicitud",
                newSchema: "transaccional");

            migrationBuilder.RenameTable(
                name: "estados_plan_pago",
                schema: "Security",
                newName: "estados_plan_pago",
                newSchema: "transaccional");

            migrationBuilder.RenameTable(
                name: "estado_cuotas",
                schema: "Security",
                newName: "estado_cuotas",
                newSchema: "transaccional");

            migrationBuilder.RenameTable(
                name: "cuotas",
                schema: "Security",
                newName: "cuotas",
                newSchema: "transaccional");

            migrationBuilder.RenameTable(
                name: "clientes",
                schema: "Security",
                newName: "clientes",
                newSchema: "transaccional");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "prestamos",
                schema: "transaccional",
                newName: "prestamos",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "planes_pago",
                schema: "transaccional",
                newName: "planes_pago",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "estados_solicitud",
                schema: "transaccional",
                newName: "estados_solicitud",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "estados_plan_pago",
                schema: "transaccional",
                newName: "estados_plan_pago",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "estado_cuotas",
                schema: "transaccional",
                newName: "estado_cuotas",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "cuotas",
                schema: "transaccional",
                newName: "cuotas",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "clientes",
                schema: "transaccional",
                newName: "clientes",
                newSchema: "Security");
        }
    }
}
