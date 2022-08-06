using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackPeliculas.Migrations
{
    public partial class _3_Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdGenero",
                table: "DetallePeliculas");

            migrationBuilder.DropColumn(
                name: "IdPelicula",
                table: "DetallePeliculas");

            migrationBuilder.DropColumn(
                name: "IdPersonajes",
                table: "DetallePeliculas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdGenero",
                table: "DetallePeliculas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdPelicula",
                table: "DetallePeliculas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdPersonajes",
                table: "DetallePeliculas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
