using BackPeliculas.Comandos.Personajes;

namespace BackPeliculas.Comandos.Peliculas;


public class ComPelicula
{
    public string Co_Imagen { get; set; } = null!;
    public string Co_Titulo { get; set;} = null!;
    public int Co_Calificacion { get; set; }

    public string Co_Genero_Nombre { get; set; } = null!;
    public List<string> Co_listaPjesNombre { get; set; } = new List<string>();
}

public class ComPeliculaId
{
    public Guid Co_IdPelicula { get; set; }
    public string Co_Imagen { get; set; } = null!;
    public string Co_Titulo { get; set;} = null!;
    public int Co_Calificacion { get; set; }

}