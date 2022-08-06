using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackPeliculas.Data;
using BackPeliculas.Resultados;
using BackPeliculas.Resultados.Personajes;

namespace BackPeliculas.Controllers;

[ApiController]
public class SearchPersonajeController : ControllerBase
{
    private readonly Context _context;
 
    public SearchPersonajeController( Context context )
    {
        _context = context;
       
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(6)
    [Route("/characters/name/{name}")]
    public async Task<ActionResult<ResDetallePje>> GetByName(string name)
    {
        var result = new ResDetallePje();

        var personaje = await _context.Personajes.Where(p => p.Nombre.Equals(name)).FirstOrDefaultAsync();
        if(personaje != null)
        {              
            result.Imagen = personaje.Imagen;
            result.Nombre = personaje.Nombre;
            result.Edad = personaje.Edad;
            result.Peso = personaje.Peso;
            result.Historia = personaje.Historia;
            
            var detPelic = await _context.DetallePeliculas.
            Include(p => p.IdPersonajesNavigation).
            Include(p => p.IdPeliculaNavigation).
            Where(p => p.IdPersonajesNavigation.Nombre == personaje.Nombre).ToListAsync();

            foreach(var item in detPelic){
                var pelicula = new ResPelicItem{
                    Imagen = item.IdPeliculaNavigation.Imagen,
                    Titulo = item.IdPeliculaNavigation.Titulo
                }; 
                result.listadoPeliculas.Add(pelicula);
            }
        }
        else{
            result.StatusCode = 404;
            result.SetError("No se encuentra personaje");
        }

        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(6)
    [Route("/characters/age/{age}")]
    public async Task<ActionResult<ResPersonajePlus>> GetByAge(int age)
    {
        var result = new ResPersonajePlus();

        var personajes = await _context.Personajes.Where(p => p.Edad.Equals(age)).ToListAsync();
        if(personajes != null)
        {              
            foreach(var item in personajes)
            {
                var resPjeItemPlus = new ResPjeItemPlus
                {
                    Imagen = item.Imagen,
                    Nombre = item.Nombre,
                    Edad = item.Edad,
                    Peso = item.Peso,
                    Historia = item.Historia
                };
                result.listaPersonajes.Add(resPjeItemPlus);
            }
            
            
            var detPelic = await _context.DetallePeliculas.
            Include(p => p.IdPersonajesNavigation).
            Include(p => p.IdPeliculaNavigation).
            Where(p => p.IdPersonajesNavigation.Edad == age).ToListAsync();

            foreach(var item in detPelic){
              
                var pelicula = new ResPelicItem{
                    Imagen = item.IdPeliculaNavigation.Imagen,
                    Titulo = item.IdPeliculaNavigation.Titulo
                };
                int c = 0;
                foreach(var item2 in result.listadoPeliculas){
                    if(pelicula.Titulo.Equals(item2.Titulo)){
                        c++;
                    }                     
                }

                if(c < 1){
                    result.listadoPeliculas.Add(pelicula);
                } 
                             
            }
        }
        else{
            result.StatusCode = 404;
            result.SetError("No se encuentra personaje");
        }

        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(6)
    [Route("/characters/weight/{weight}")]
    public async Task<ActionResult<ResPersonajePlus>> GetByWeight(int weight)
    {
        var result = new ResPersonajePlus();

        var personajes = await _context.Personajes.Where(p => p.Peso.Equals(weight)).ToListAsync();
        if(personajes != null)
        {              
            foreach(var item in personajes)
            {
                var resPjeItemPlus = new ResPjeItemPlus
                {
                    Imagen = item.Imagen,
                    Nombre = item.Nombre,
                    Edad = item.Edad,
                    Peso = item.Peso,
                    Historia = item.Historia
                };
                result.listaPersonajes.Add(resPjeItemPlus);
            }
            
            
            var detPelic = await _context.DetallePeliculas.
            Include(p => p.IdPersonajesNavigation).
            Include(p => p.IdPeliculaNavigation).
            Where(p => p.IdPersonajesNavigation.Peso == weight).ToListAsync();

            foreach(var item in detPelic){
              
                var pelicula = new ResPelicItem{
                    Imagen = item.IdPeliculaNavigation.Imagen,
                    Titulo = item.IdPeliculaNavigation.Titulo
                };
                
                int c = 0;
                foreach(var item2 in result.listadoPeliculas){  //control de duplicados
                    if(pelicula.Titulo.Equals(item2.Titulo)){
                        c++;
                    }                     
                }

                if(c < 1){
                    result.listadoPeliculas.Add(pelicula);
                } 
                             
            }
        }
        else{
            result.StatusCode = 404;
            result.SetError("No se encuentra personaje");
        }

        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(6)
    [Route("/characters/movies/{idMovie}")]
    public async Task<ActionResult<ResPersonaje>> GetByWIdMovie(Guid idMovie)
    {
        var result = new ResPersonaje();

        var detPelic = await _context.DetallePeliculas.
            Include(p => p.IdPersonajesNavigation).
            Include(p => p.IdPeliculaNavigation).
            Where(p => p.IdPeliculaNavigation.PeliculaId == idMovie).ToListAsync();

        if(detPelic != null){
            foreach(var item in detPelic){
            var resPjeItem = new ResPjeItem{
                Imagen = item.IdPersonajesNavigation.Imagen,
                Nombre = item.IdPersonajesNavigation.Nombre
            };
            result.listaPersonajes.Add(resPjeItem);
            }
        }
        else{
            result.StatusCode = 404;
            result.SetError("No existe película");
        }
        
        return result;
    }

} 