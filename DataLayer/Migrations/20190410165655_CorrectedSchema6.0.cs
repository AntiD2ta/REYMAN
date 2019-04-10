using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CorrectedSchema60 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccionC_Material_AccionesCons_AccionConstructivaID",
                table: "AccionC_Material");

            migrationBuilder.DropForeignKey(
                name: "FK_AccionC_Material_Materiales_MaterialID",
                table: "AccionC_Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Especialidades_UnidadesOrganizativas_UnidadOrganizativaID",
                table: "Especialidades");

            migrationBuilder.DropIndex(
                name: "IX_Especialidades_UnidadOrganizativaID",
                table: "Especialidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccionC_Material",
                table: "AccionC_Material");

            migrationBuilder.DropColumn(
                name: "UnidadOrganizativaID",
                table: "Especialidades");

            migrationBuilder.RenameTable(
                name: "AccionC_Material",
                newName: "AccCons_Mat");

            migrationBuilder.RenameIndex(
                name: "IX_AccionC_Material_MaterialID",
                table: "AccCons_Mat",
                newName: "IX_AccCons_Mat_MaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_AccionC_Material_AccionConstructivaID",
                table: "AccCons_Mat",
                newName: "IX_AccCons_Mat_AccionConstructivaID");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Planes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnidadOrganizativaID",
                table: "Inmuebles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidadOrganizativaID1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                table: "AccCons_Mat",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccCons_Mat",
                table: "AccCons_Mat",
                column: "AccionC_MaterialID");

            migrationBuilder.CreateTable(
                name: "PlanesActuales",
                columns: table => new
                {
                    PlanActualID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnidadOrganizativaID = table.Column<int>(nullable: true),
                    PlanID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesActuales", x => x.PlanActualID);
                    table.ForeignKey(
                        name: "FK_PlanesActuales_Planes_PlanID",
                        column: x => x.PlanID,
                        principalTable: "Planes",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanesActuales_UnidadesOrganizativas_UnidadOrganizativaID",
                        column: x => x.UnidadOrganizativaID,
                        principalTable: "UnidadesOrganizativas",
                        principalColumn: "UnidadOrganizativaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inmuebles_UnidadOrganizativaID",
                table: "Inmuebles",
                column: "UnidadOrganizativaID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnidadOrganizativaID1",
                table: "AspNetUsers",
                column: "UnidadOrganizativaID1");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesActuales_PlanID",
                table: "PlanesActuales",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesActuales_UnidadOrganizativaID",
                table: "PlanesActuales",
                column: "UnidadOrganizativaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccCons_Mat_AccionesCons_AccionConstructivaID",
                table: "AccCons_Mat",
                column: "AccionConstructivaID",
                principalTable: "AccionesCons",
                principalColumn: "AccionConstructivaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccCons_Mat_Materiales_MaterialID",
                table: "AccCons_Mat",
                column: "MaterialID",
                principalTable: "Materiales",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UnidadesOrganizativas_UnidadOrganizativaID1",
                table: "AspNetUsers",
                column: "UnidadOrganizativaID1",
                principalTable: "UnidadesOrganizativas",
                principalColumn: "UnidadOrganizativaID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Inmuebles_UnidadesOrganizativas_UnidadOrganizativaID",
                table: "Inmuebles",
                column: "UnidadOrganizativaID",
                principalTable: "UnidadesOrganizativas",
                principalColumn: "UnidadOrganizativaID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccCons_Mat_AccionesCons_AccionConstructivaID",
                table: "AccCons_Mat");

            migrationBuilder.DropForeignKey(
                name: "FK_AccCons_Mat_Materiales_MaterialID",
                table: "AccCons_Mat");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UnidadesOrganizativas_UnidadOrganizativaID1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Inmuebles_UnidadesOrganizativas_UnidadOrganizativaID",
                table: "Inmuebles");

            migrationBuilder.DropTable(
                name: "PlanesActuales");

            migrationBuilder.DropIndex(
                name: "IX_Inmuebles_UnidadOrganizativaID",
                table: "Inmuebles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UnidadOrganizativaID1",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccCons_Mat",
                table: "AccCons_Mat");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "UnidadOrganizativaID",
                table: "Inmuebles");

            migrationBuilder.DropColumn(
                name: "UnidadOrganizativaID1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "AccCons_Mat");

            migrationBuilder.RenameTable(
                name: "AccCons_Mat",
                newName: "AccionC_Material");

            migrationBuilder.RenameIndex(
                name: "IX_AccCons_Mat_MaterialID",
                table: "AccionC_Material",
                newName: "IX_AccionC_Material_MaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_AccCons_Mat_AccionConstructivaID",
                table: "AccionC_Material",
                newName: "IX_AccionC_Material_AccionConstructivaID");

            migrationBuilder.AddColumn<int>(
                name: "UnidadOrganizativaID",
                table: "Especialidades",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccionC_Material",
                table: "AccionC_Material",
                column: "AccionC_MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Especialidades_UnidadOrganizativaID",
                table: "Especialidades",
                column: "UnidadOrganizativaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccionC_Material_AccionesCons_AccionConstructivaID",
                table: "AccionC_Material",
                column: "AccionConstructivaID",
                principalTable: "AccionesCons",
                principalColumn: "AccionConstructivaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccionC_Material_Materiales_MaterialID",
                table: "AccionC_Material",
                column: "MaterialID",
                principalTable: "Materiales",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Especialidades_UnidadesOrganizativas_UnidadOrganizativaID",
                table: "Especialidades",
                column: "UnidadOrganizativaID",
                principalTable: "UnidadesOrganizativas",
                principalColumn: "UnidadOrganizativaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
