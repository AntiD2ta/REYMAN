using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class KarlLewis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Pais_Visa");

            migrationBuilder.DropTable(
                name: "Pasaporte_Visa");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Pasaportes");

            migrationBuilder.DropTable(
                name: "Visa");

            migrationBuilder.DropTable(
                name: "Viajes");

            migrationBuilder.AddColumn<int>(
                name: "UnidadOrganizativaID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Presupuesto = table.Column<decimal>(nullable: false),
                    Año = table.Column<int>(nullable: false),
                    TipoPlan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    ProvinciaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.ProvinciaID);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesOrganizativas",
                columns: table => new
                {
                    UnidadOrganizativaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    ProvinciaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesOrganizativas", x => x.UnidadOrganizativaID);
                    table.ForeignKey(
                        name: "FK_UnidadesOrganizativas_Provincias_ProvinciaID",
                        column: x => x.ProvinciaID,
                        principalTable: "Provincias",
                        principalColumn: "ProvinciaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inmuebles",
                columns: table => new
                {
                    InmuebleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Direccion = table.Column<string>(nullable: true),
                    UOUnidadOrganizativaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inmuebles", x => x.InmuebleID);
                    table.ForeignKey(
                        name: "FK_Inmuebles_UnidadesOrganizativas_UOUnidadOrganizativaID",
                        column: x => x.UOUnidadOrganizativaID,
                        principalTable: "UnidadesOrganizativas",
                        principalColumn: "UnidadOrganizativaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObjetosObra",
                columns: table => new
                {
                    ObjetoObraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InmuebleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjetosObra", x => x.ObjetoObraID);
                    table.ForeignKey(
                        name: "FK_ObjetosObra_Inmuebles_InmuebleID",
                        column: x => x.InmuebleID,
                        principalTable: "Inmuebles",
                        principalColumn: "InmuebleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccionesCons",
                columns: table => new
                {
                    AccionConstructivaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Especialidad = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    ObjetoObraID = table.Column<int>(nullable: true),
                    PlanID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccionesCons", x => x.AccionConstructivaID);
                    table.ForeignKey(
                        name: "FK_AccionesCons_ObjetosObra_ObjetoObraID",
                        column: x => x.ObjetoObraID,
                        principalTable: "ObjetosObra",
                        principalColumn: "ObjetoObraID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccionesCons_Planes_PlanID",
                        column: x => x.PlanID,
                        principalTable: "Planes",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManosObra",
                columns: table => new
                {
                    ManoObraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(nullable: false),
                    UnidadMedida = table.Column<string>(nullable: true),
                    Precio = table.Column<decimal>(nullable: false),
                    AccionConstructivaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManosObra", x => x.ManoObraID);
                    table.ForeignKey(
                        name: "FK_ManosObra_AccionesCons_AccionConstructivaID",
                        column: x => x.AccionConstructivaID,
                        principalTable: "AccionesCons",
                        principalColumn: "AccionConstructivaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    MaterialID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    UnidadMedida = table.Column<string>(nullable: true),
                    AccionesConstructivasAccionConstructivaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.MaterialID);
                    table.ForeignKey(
                        name: "FK_Materiales_AccionesCons_AccionesConstructivasAccionConstructivaID",
                        column: x => x.AccionesConstructivasAccionConstructivaID,
                        principalTable: "AccionesCons",
                        principalColumn: "AccionConstructivaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnidadOrganizativaID",
                table: "AspNetUsers",
                column: "UnidadOrganizativaID");

            migrationBuilder.CreateIndex(
                name: "IX_AccionesCons_ObjetoObraID",
                table: "AccionesCons",
                column: "ObjetoObraID");

            migrationBuilder.CreateIndex(
                name: "IX_AccionesCons_PlanID",
                table: "AccionesCons",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Inmuebles_UOUnidadOrganizativaID",
                table: "Inmuebles",
                column: "UOUnidadOrganizativaID");

            migrationBuilder.CreateIndex(
                name: "IX_ManosObra_AccionConstructivaID",
                table: "ManosObra",
                column: "AccionConstructivaID");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_AccionesConstructivasAccionConstructivaID",
                table: "Materiales",
                column: "AccionesConstructivasAccionConstructivaID");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetosObra_InmuebleID",
                table: "ObjetosObra",
                column: "InmuebleID");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesOrganizativas_ProvinciaID",
                table: "UnidadesOrganizativas",
                column: "ProvinciaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UnidadesOrganizativas_UnidadOrganizativaID",
                table: "AspNetUsers",
                column: "UnidadOrganizativaID",
                principalTable: "UnidadesOrganizativas",
                principalColumn: "UnidadOrganizativaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UnidadesOrganizativas_UnidadOrganizativaID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ManosObra");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "AccionesCons");

            migrationBuilder.DropTable(
                name: "ObjetosObra");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Inmuebles");

            migrationBuilder.DropTable(
                name: "UnidadesOrganizativas");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UnidadOrganizativaID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UnidadOrganizativaID",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Pasaportes",
                columns: table => new
                {
                    PasaporteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Actualizaciones = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    UsuarioCI = table.Column<long>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasaportes", x => x.PasaporteID);
                    table.ForeignKey(
                        name: "FK_Pasaportes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Viajes",
                columns: table => new
                {
                    ViajeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Costo = table.Column<int>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    MotivoViaje = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viajes", x => x.ViajeID);
                    table.ForeignKey(
                        name: "FK_Viajes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visa",
                columns: table => new
                {
                    VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visa", x => x.VisaID);
                });

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    InstitucionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.InstitucionID);
                    table.ForeignKey(
                        name: "FK_Instituciones_Viajes_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viajes",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    PaisID = table.Column<string>(nullable: false),
                    ViajeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisID);
                    table.ForeignKey(
                        name: "FK_Paises_Viajes_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viajes",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pasaporte_Visa",
                columns: table => new
                {
                    Pasaporte_VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PasaporteID = table.Column<int>(nullable: true),
                    VisaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasaporte_Visa", x => x.Pasaporte_VisaID);
                    table.ForeignKey(
                        name: "FK_Pasaporte_Visa_Pasaportes_PasaporteID",
                        column: x => x.PasaporteID,
                        principalTable: "Pasaportes",
                        principalColumn: "PasaporteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pasaporte_Visa_Visa_VisaID",
                        column: x => x.VisaID,
                        principalTable: "Visa",
                        principalColumn: "VisaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    CiudadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    PaisID = table.Column<string>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.CiudadID);
                    table.ForeignKey(
                        name: "FK_Ciudades_Paises_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Paises",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ciudades_Viajes_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viajes",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pais_Visa",
                columns: table => new
                {
                    Pais_VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaisID = table.Column<string>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true),
                    VisaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais_Visa", x => x.Pais_VisaID);
                    table.ForeignKey(
                        name: "FK_Pais_Visa_Paises_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Paises",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pais_Visa_Viajes_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viajes",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pais_Visa_Visa_VisaID",
                        column: x => x.VisaID,
                        principalTable: "Visa",
                        principalColumn: "VisaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_PaisID",
                table: "Ciudades",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_ViajeID",
                table: "Ciudades",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Instituciones_ViajeID",
                table: "Instituciones",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Visa_PaisID",
                table: "Pais_Visa",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Visa_ViajeID",
                table: "Pais_Visa",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Visa_VisaID",
                table: "Pais_Visa",
                column: "VisaID");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_ViajeID",
                table: "Paises",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaporte_Visa_PasaporteID",
                table: "Pasaporte_Visa",
                column: "PasaporteID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaporte_Visa_VisaID",
                table: "Pasaporte_Visa",
                column: "VisaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaportes_UsuarioId",
                table: "Pasaportes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_UsuarioId",
                table: "Viajes",
                column: "UsuarioId");
        }
    }
}
