using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CorrectedSchema40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManosObra_UnidadMedida_UnidadMedidaID",
                table: "ManosObra");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_UnidadMedida_UnidadMedidaID",
                table: "Materiales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadMedida",
                table: "UnidadMedida");

            migrationBuilder.RenameTable(
                name: "UnidadMedida",
                newName: "UnidadesMedida");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "AccionC_Material",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadesMedida",
                table: "UnidadesMedida",
                column: "UnidadMedidaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ManosObra_UnidadesMedida_UnidadMedidaID",
                table: "ManosObra",
                column: "UnidadMedidaID",
                principalTable: "UnidadesMedida",
                principalColumn: "UnidadMedidaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_UnidadesMedida_UnidadMedidaID",
                table: "Materiales",
                column: "UnidadMedidaID",
                principalTable: "UnidadesMedida",
                principalColumn: "UnidadMedidaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManosObra_UnidadesMedida_UnidadMedidaID",
                table: "ManosObra");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_UnidadesMedida_UnidadMedidaID",
                table: "Materiales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadesMedida",
                table: "UnidadesMedida");

            migrationBuilder.RenameTable(
                name: "UnidadesMedida",
                newName: "UnidadMedida");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "AccionC_Material",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadMedida",
                table: "UnidadMedida",
                column: "UnidadMedidaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ManosObra_UnidadMedida_UnidadMedidaID",
                table: "ManosObra",
                column: "UnidadMedidaID",
                principalTable: "UnidadMedida",
                principalColumn: "UnidadMedidaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_UnidadMedida_UnidadMedidaID",
                table: "Materiales",
                column: "UnidadMedidaID",
                principalTable: "UnidadMedida",
                principalColumn: "UnidadMedidaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
