using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackPeliculas.Models;
using BackPeliculas.Data;
using BackPeliculas.Resultados;
using BackPeliculas.Comandos.Usuarios;



namespace BackPeliculas.Controllers;

public class UsuarioController
{

    private readonly Context _context;
    public UsuarioController(Context context) { 
        _context = context; 

    }
    //---------------------------------------------------------------------------------------------------------
    [HttpPost]  //(2)
    [Route("/auth/register")]
    public async Task<ActionResult<ResultadoBase>> UserRegister([FromBody] ComUsuario comando)
    {
        var result = new ResultadoBase();

        try {
            var usuarios = await _context.Usuarios.Where(u => u.NombreUsuario.Equals(comando.NombreUsuario)).FirstOrDefaultAsync();

            if(usuarios == null){
                var nuevoUsuario = new Usuario{
                UsuarioId = Guid.NewGuid(),
                NombreUsuario = comando.NombreUsuario,
                Password = comando.Password
                };
                await _context.AddAsync(nuevoUsuario);
                await _context.SaveChangesAsync();

                result.StatusCode = 200;
            }
            else{
                result.StatusCode = 404;
                result.SetError("Nombre de usuario ya existe");
            }
            

        }
        catch(Exception e){
            result.StatusCode = 400;
            result.SetError("" + e);
        }
        
        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpPost]  //(2)
    [Route("/auth/login")]
    public async Task<ActionResult<ResultadoBase>> UserLogIn([FromBody] ComUsuario comando)
    {
        var result = new ResultadoBase();

        var usuario = await _context.Usuarios.Where(x => 
        x.NombreUsuario.Equals(comando.NombreUsuario) && 
        x.Password.Equals(comando.Password)).FirstOrDefaultAsync();

        if(usuario != null){
            result.StatusCode = 200;
        }
        else{
            result.SetError("Usuario no encontrado en BD"); 
            result.StatusCode = 500;
        }


        return result;
    }
}