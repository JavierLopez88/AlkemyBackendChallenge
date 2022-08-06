namespace BackPeliculas.Comandos.Personajes;

public class ComPersonaje 
{
    public Guid Com_IdPersonaje { get; set; }
    public string Com_Imagen { get; set; } = null!;
    public string Com_Nombre { get; set;} = null!;
    public int Com_Edad { get; set; } 
    public float Com_Peso { get; set; }
    public string Com_Historia { get; set; } = null!;
}

