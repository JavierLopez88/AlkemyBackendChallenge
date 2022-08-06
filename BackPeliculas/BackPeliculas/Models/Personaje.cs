namespace BackPeliculas.Models;

public class Personaje
{
    public Guid PersonajeId { get; set; }
    public string Imagen { get; set; } = null!;
    public string Nombre { get; set;} = null!;
    public int Edad { get; set; } 
    public float Peso { get; set; }
    public string Historia { get; set; } = null!;

    public virtual ICollection<DetallePelicula> DetallePeliculas { get; set; }= null!;

}
