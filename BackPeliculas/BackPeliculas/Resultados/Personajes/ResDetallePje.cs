
namespace BackPeliculas.Resultados.Personajes;


public class ResDetallePje : ResultadoBase
{

   public string Imagen { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public int Edad { get; set; } 
    public float Peso { get; set; }
    public string Historia { get; set; } = null!;
    public List<ResPelicItem> listadoPeliculas { get; set; } = new List<ResPelicItem>();
}


