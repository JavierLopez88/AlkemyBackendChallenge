using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackPeliculas.Data;
using BackPeliculas.Resultados;
using BackPeliculas.Resultados.Personajes;

namespace BackPeliculas.Controllers;

[ApiController]
public class SearchPeliculaController : ControllerBase
{
    private readonly Context _context;

    public SearchPeliculaController(Context context)
    {
        _context = context;

    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(10)
    [Route("/movies/name/{name}")]
    public async Task<ActionResult<ResPelicItem>> GetPeliculaByName(string name)
    {
        var pelicula = await _context.Peliculas.Where(p => p.Titulo == name).FirstOrDefaultAsync();
        if (pelicula != null)
        {
            var result = new ResPelicItem
            {
                Imagen = pelicula.Imagen,
                Titulo = pelicula.Titulo,
                Calificacion = pelicula.Calificacion
            };
            return result;
        }
        else
        {
            return BadRequest("No se encuentra Pelicula");
        }

    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(10)
    [Route("/movies/gender/{idGen}")]
    public async Task<ActionResult<ResPelicula>> GetPeliculaByGender(Guid idGen)
    {
        var result = new ResPelicula();

        var detalle = await _context.DetallePeliculas.Where(d => d.IdGeneroNavigationGeneroId == idGen).ToListAsync();

        if (detalle != null)
        {
            foreach (var item in detalle)
            {
                var pelicula = await _context.Peliculas.
                Where(p => p.PeliculaId.Equals(item.IdPeliculaNavigationPeliculaId)).FirstOrDefaultAsync();

                if (pelicula != null)
                {
                    int c = 0;
                    foreach (var item2 in result.listaPeliculas){                       
                        if (item.IdPeliculaNavigationPeliculaId.Equals(item2.PeliculaId)){                       
                            c++;                           
                        }
                    }        

                    if(c < 1){
                        var peli = new ResPelicItemPlus
                        {
                            PeliculaId = pelicula.PeliculaId,
                            Imagen = pelicula.Imagen,
                            Titulo = pelicula.Titulo
                        };
                        result.listaPeliculas.Add(peli);
                    }
                }
            }
            result.StatusCode = 200;
        }
        else
        {
            result.StatusCode = 200;
            result.SetError("No se encuentran peliculas para ese género");
        }
        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(10)
    [Route("/movies/order/{order}")]
    public async Task<ActionResult<ResPelicula>> GetPeliculaByOrder(string order)
    {
        var result = new ResPelicula();
        var peliculas = new List<Models.Pelicula>();
       
        if(order == "ASC") 
            peliculas = await _context.Peliculas.OrderBy(x => x.Titulo).ToListAsync();
        else if(order == "DESC")
            peliculas = await _context.Peliculas.OrderByDescending(x => x.Titulo).ToListAsync();    
        else 
            return BadRequest("Parámetro incorrecto");
        

        if (peliculas != null){
            foreach (var item in peliculas){
                var peli = new ResPelicItemPlus{
                    PeliculaId = item.PeliculaId,
                    Imagen = item.Imagen,
                    Titulo = item.Titulo
                };
                result.listaPeliculas.Add(peli);
            }
            result.StatusCode = 200;
        }
        else{
            result.StatusCode = 200;
            result.SetError("No se encuentran peliculas");
        }
        return result; 

    }

    //---------------------------------------------------------------------------------------------------------
}