using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CorrectedSchema30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManosObra_AccionesCons_AccionConstructivaID",
                table: "ManosObra");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "ManosObra");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "AccionesCons");

            migrationBuilder.RenameColumn(
                name: "AccionConstructivaID",
                table: "ManosObra",
                newName: "UnidadMedidaID");

            migrationBuilder.RenameIndex(
                name: "IX_ManosObra_AccionConstructivaID",
                table: "ManosObra",
                newName: "IX_ManosObra_UnidadMedidaID");

            migrationBuilder.AddColumn<int>(
                name: "UnidadMedidaID",
                table: "Materiales",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "ManosObra",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadID",
                table: "AccionesCons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManoObraID",
                table: "AccionesCons",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "AccionC_Material",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    UnidadMedidaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.UnidadMedidaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_UnidadMedidaID",
                table: "Materiales",
                column: "UnidadMedidaID");

            migrationBuilder.CreateIndex(
                name: "IX_AccionesCons_EspecialidadID",
                table: "AccionesCons",
                column: "EspecialidadID");

            migrationBuilder.CreateIndex(
                name: "IX_AccionesCons_ManoObraID",
                table: "AccionesCons",
                column: "ManoObraID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccionesCons_Especialidades_EspecialidadID",
                table: "AccionesCons",
                column: "EspecialidadID",
                principalTable: "Especialidades",
                principalColumn: "EspecialidadID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccionesCons_ManosObra_ManoObraID",
                table: "AccionesCons",
                column: "ManoObraID",
                principalTable: "ManosObra",
                principalColumn: "ManoObraID",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccionesCons_Especialidades_EspecialidadID",
                table: "AccionesCons");

            migrationBuilder.DropForeignKey(
                name: "FK_AccionesCons_ManosObra_ManoObraID",
                table: "AccionesCons");

            migrationBuilder.DropForeignKey(
                name: "FK_ManosObra_UnidadMedida_UnidadMedidaID",
                table: "ManosObra");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_UnidadMedida_UnidadMedidaID",
                table: "Materiales");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropIndex(
                name: "IX_Materiales_UnidadMedidaID",
                table: "Materiales");

            migrationBuilder.DropIndex(
                name: "IX_AccionesCons_EspecialidadID",
                table: "AccionesCons");

            migrationBuilder.DropIndex(
                name: "IX_AccionesCons_ManoObraID",
                table: "AccionesCons");

            migrationBuilder.DropColumn(
                name: "UnidadMedidaID",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "EspecialidadID",
                table: "AccionesCons");

            migrationBuilder.DropColumn(
                name: "ManoObraID",
                table: "AccionesCons");

            migrationBuilder.RenameColumn(
                name: "UnidadMedidaID",
                table: "ManosObra",
                newName: "AccionConstructivaID");

            migrationBuilder.RenameIndex(
                name: "IX_ManosObra_UnidadMedidaID",
                table: "ManosObra",
                newName: "IX_ManosObra_AccionConstructivaID");

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "Materiales",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Precio",
                table: "ManosObra",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "ManosObra",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Especialidad",
                table: "AccionesCons",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Precio",
                table: "AccionC_Material",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddForeignKey(
                name: "FK_ManosObra_AccionesCons_AccionConstructivaID",
                table: "ManosObra",
                column: "AccionConstructivaID",
                principalTable: "AccionesCons",
                principalColumn: "AccionConstructivaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
