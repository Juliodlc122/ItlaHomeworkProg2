using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutletRopa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoHijos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Productos",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EsDeportivo",
                table: "Productos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsMangaLarga",
                table: "Productos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TieneBolsillos",
                table: "Productos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCorte",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCuello",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "EsDeportivo",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "EsMangaLarga",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TieneBolsillos",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TipoCorte",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TipoCuello",
                table: "Productos");
        }
    }
}
