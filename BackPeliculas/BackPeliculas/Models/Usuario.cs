namespace BackPeliculas.Models;

public class Usuario
{
    public Guid UsuarioId { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public string Password { get; set;} = null!;
}
