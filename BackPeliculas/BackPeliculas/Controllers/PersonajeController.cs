using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BackPeliculas.Data;
using BackPeliculas.Models;
using BackPeliculas.Resultados;
using BackPeliculas.Resultados.Personajes;
using BackPeliculas.Comandos.Personajes;



namespace BackPeliculas.Controllers;

[ApiController]
public class PersonajeController : ControllerBase
{
    private readonly Context _context;
 
    public PersonajeController( Context context )
    {
        _context = context;
       
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet] //(3)
    [Route("/characters")]
    public async Task<ActionResult<ResPersonaje>> GetPersonajes()
    {
        var personajes = await _context.Personajes.ToListAsync();

        var resultPersonaje = new ResPersonaje();

        if (personajes != null)
        {
            foreach (var item in personajes)
            {
                var resPjeItem = new ResPjeItem();
                resPjeItem.Id = item.PersonajeId;
                resPjeItem.Imagen = item.Imagen;
                resPjeItem.Nombre = item.Nombre;

                resultPersonaje.listaPersonajes.Add(resPjeItem);
                resultPersonaje.StatusCode = 200;
            }
            return resultPersonaje;
        }
        else
        {
            resultPersonaje.StatusCode = 400;
            resultPersonaje.SetError("No existen personajes");
            resultPersonaje.Ok = false;
            return resultPersonaje;
        }
    }


    //---------------------------------------------------------------------------------------------------------
    [HttpPost]  //(4)
    [Route("/characters/create")]
    public async Task<ActionResult<ResPersonaje>> InsertPersonaje([FromBody] ComPersonaje comando)
    {
        var resultado = new ResultadoBase();
        try
        {
            var nuevo = new Personaje();
            nuevo.PersonajeId = Guid.NewGuid();
            nuevo.Imagen = comando.Com_Imagen;
            nuevo.Nombre = comando.Com_Nombre;
            nuevo.Edad = comando.Com_Edad;
            nuevo.Peso = comando.Com_Peso;
            nuevo.Historia = comando.Com_Historia;

            await _context.AddAsync(nuevo);
            await _context.SaveChangesAsync();

            resultado.StatusCode = 201;
            return Ok(resultado);

        }
        catch (Exception)
        {
            resultado.SetError("No puede crearse Personaje");
            resultado.StatusCode = 404;
            return BadRequest(resultado);
        }

    }
    //---------------------------------------------------------------------------------------------------------
    [HttpPut]  //(4)
    [Route("/characters/update")]
    public async Task<ActionResult<ResultadoBase>> UpdatePersonaje([FromBody] ComPersonaje comando)
    {
        var resultado = new ResultadoBase();

        var personaje = await _context.Personajes.Where(p =>
        p.PersonajeId.Equals(comando.Com_IdPersonaje)).FirstOrDefaultAsync();

        if (personaje != null)
        {
            personaje.Imagen = comando.Com_Imagen;
            personaje.Nombre = comando.Com_Nombre;
            personaje.Edad = comando.Com_Edad;
            personaje.Peso = comando.Com_Peso;
            personaje.Historia = comando.Com_Historia;

            _context.Update(personaje);
            await _context.SaveChangesAsync();

            resultado.StatusCode = 200;
        }
        else
        {
            resultado.SetError("No se encuentra personaje");
            resultado.StatusCode = 500;

        }

        return resultado;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpDelete] //(4)
    [Route("/characters/delete/{paramId}")]
    public async Task<ActionResult<ResultadoBase>> Delete(Guid paramId)
    {
        var personaje = await _context.Personajes.Where(p => p.PersonajeId == paramId).FirstOrDefaultAsync();
        var resultado = new ResultadoBase();

        if (personaje != null)
        {
            _context.Remove(personaje);
            await _context.SaveChangesAsync();
            resultado.StatusCode = 200;
            return Ok(resultado);
        }
        else
        {
            resultado.StatusCode = 404;
            resultado.SetError("Error, no se encuentra personaje");
            return resultado;
        }
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(5)
    [Route("/characters/detallePersonaje/{paramId}")]
    public async Task<ActionResult<ResDetallePje>> DetallePersonaje(Guid paramId)
    {
        var result = new ResDetallePje();

        var personaje = await _context.Personajes.Where(p => p.PersonajeId.Equals(paramId)).FirstOrDefaultAsync();
        if (personaje != null)
        {
            result.Imagen = personaje.Imagen;
            result.Nombre = personaje.Nombre;
            result.Edad = personaje.Edad;
            result.Peso = personaje.Peso;
            result.Historia = personaje.Historia;
        }
        else
        {
            result.StatusCode = 404;
            result.SetError("No se encuentra personaje");          
        }

        var detPelic = await _context.DetallePeliculas.
        Include(p => p.IdPeliculaNavigation).Include(p => p.IdPersonajesNavigation).ToListAsync();

        if (detPelic != null)
        {
            foreach (var item in detPelic)
            {
                if (item.IdPersonajesNavigation.PersonajeId.Equals(paramId))
                {
                    ResPelicItem pelicula = new ResPelicItem
                    {
                        Imagen = item.IdPeliculaNavigation.Imagen,
                        Titulo = item.IdPeliculaNavigation.Titulo
                    };
                    result.listadoPeliculas.Add(pelicula);
                }
            }
        }
        else
        {
            result.StatusCode = 404;
            result.SetError("No se encuentra personaje");
        }

        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    
}