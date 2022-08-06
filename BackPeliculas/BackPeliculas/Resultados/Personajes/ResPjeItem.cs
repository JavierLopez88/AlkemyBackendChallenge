namespace BackPeliculas.Resultados.Personajes;

public class ResPjeItemPlus 
{
    public string Imagen { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public int Edad { get; set; } 
    public float Peso { get; set; }
    public string Historia { get; set; } = null!;
}

public class ResPjeItem 
{
    public Guid Id { get; set; }
    public string Imagen { get; set; } = null!;
    public string Nombre { get; set; } = null!;
}