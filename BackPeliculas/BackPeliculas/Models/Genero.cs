namespace BackPeliculas.Models;

public class Genero
{
    public Guid GeneroId { get; set; }
    public string Imagen { get; set; } = null!;
    public string Nombre { get; set;} = null!;

    public virtual ICollection<DetallePelicula> DetallePeliculas { get; set; }= null!;


   

}


