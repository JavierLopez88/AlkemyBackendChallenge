using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackPeliculas.Migrations
{
    public partial class _1_Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    GeneroId = table.Column<Guid>(type: "uuid", nullable: false),
                    Imagen = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    PeliculaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Imagen = table.Column<string>(type: "text", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    FechaAlta = table.Column<DateOnly>(type: "date", nullable: false),
                    Calificacion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.PeliculaId);
                });

            migrationBuilder.CreateTable(
                name: "Personajes",
                columns: table => new
                {
                    PersonajeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Imagen = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Historia = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personajes", x => x.PersonajeId);
                });

            migrationBuilder.CreateTable(
                name: "DetallePeliculas",
                columns: table => new
                {
                    DetallePeliculaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPelicula = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPeliculaNavigationPeliculaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPersonajes = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPersonajesNavigationPersonajeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdGenero = table.Column<Guid>(type: "uuid", nullable: false),
                    IdGeneroNavigationGeneroId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePeliculas", x => x.DetallePeliculaId);
                    table.ForeignKey(
                        name: "FK_DetallePeliculas_Generos_IdGeneroNavigationGeneroId",
                        column: x => x.IdGeneroNavigationGeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePeliculas_Peliculas_IdPeliculaNavigationPeliculaId",
                        column: x => x.IdPeliculaNavigationPeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "PeliculaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePeliculas_Personajes_IdPersonajesNavigationPersonaje~",
                        column: x => x.IdPersonajesNavigationPersonajeId,
                        principalTable: "Personajes",
                        principalColumn: "PersonajeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePeliculas_IdGeneroNavigationGeneroId",
                table: "DetallePeliculas",
                column: "IdGeneroNavigationGeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePeliculas_IdPeliculaNavigationPeliculaId",
                table: "DetallePeliculas",
                column: "IdPeliculaNavigationPeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePeliculas_IdPersonajesNavigationPersonajeId",
                table: "DetallePeliculas",
                column: "IdPersonajesNavigationPersonajeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePeliculas");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Personajes");
        }
    }
}
