namespace BackPeliculas.Resultados;

public class ResPelicItem
{
    public string Imagen { get; set; } = null!;
    public string Titulo { get; set;} = null!;
    public int Calificacion { get; set; }

}

public class ResPelicItemPlus
{
    public Guid PeliculaId { get; set; }
    public string Imagen { get; set; } = null!;
    public string Titulo { get; set;} = null!;

    

}