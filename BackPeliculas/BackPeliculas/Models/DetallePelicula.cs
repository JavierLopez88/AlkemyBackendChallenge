namespace BackPeliculas.Models;

public class DetallePelicula
{
    public Guid DetallePeliculaId { get; set; }  
    public Guid IdPeliculaNavigationPeliculaId { get; set; }  
    public Guid IdPersonajesNavigationPersonajeId { get; set; }  
    public Guid IdGeneroNavigationGeneroId { get; set; }  


    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;
    public virtual Personaje IdPersonajesNavigation { get; set; } = null!;
    public virtual Genero IdGeneroNavigation { get; set; } = null!;

}
