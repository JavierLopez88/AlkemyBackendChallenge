namespace BackPeliculas.Models;

public class Pelicula
{
    public Guid PeliculaId { get; set; }
    public string Imagen { get; set; } = null!;
    public string Titulo { get; set;} = null!;
    public DateOnly FechaAlta { get; set; }
    public int Calificacion { get; set; } //1-5

    public virtual ICollection<DetallePelicula> DetallePeliculas { get; set; }= null!;


}
