using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CorrectedSchema50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "ManosObra");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "AccionC_Material",
                newName: "PrecioCUP");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCUC",
                table: "ManosObra",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCUP",
                table: "ManosObra",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCUC",
                table: "AccionC_Material",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioCUC",
                table: "ManosObra");

            migrationBuilder.DropColumn(
                name: "PrecioCUP",
                table: "ManosObra");

            migrationBuilder.DropColumn(
                name: "PrecioCUC",
                table: "AccionC_Material");

            migrationBuilder.RenameColumn(
                name: "PrecioCUP",
                table: "AccionC_Material",
                newName: "Precio");

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "ManosObra",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
