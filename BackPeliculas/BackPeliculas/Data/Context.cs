using Microsoft.EntityFrameworkCore;
using BackPeliculas.Models;

using BackPeliculas.Data;
namespace BackPeliculas.Data;

public class Context : DbContext  //(1)
{
    public Context(DbContextOptions<Context> options) : base(options)
    {


    }

    public virtual DbSet<DetallePelicula> DetallePeliculas { get; set; } = null!;
    public virtual DbSet<Genero> Generos { get; set; } = null!;
    public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;
    public virtual DbSet<Personaje> Personajes { get; set; } = null!;

    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //  {
    //      options.UseNpgsql("ConexionDatabase");
    //  }

}
