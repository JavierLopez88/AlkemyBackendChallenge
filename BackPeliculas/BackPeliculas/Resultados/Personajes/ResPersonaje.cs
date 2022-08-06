namespace BackPeliculas.Resultados.Personajes;

public class ResPersonaje : ResultadoBase
{
    public List<ResPjeItem> listaPersonajes { get; set;} = new List<ResPjeItem>();

}

public class ResPersonajePlus : ResultadoBase
{
    public List<ResPjeItemPlus> listaPersonajes { get; set;} = new List<ResPjeItemPlus>();
    public List<ResPelicItem> listadoPeliculas { get; set; } = new List<ResPelicItem>();

}