using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackPeliculas.Models;
using BackPeliculas.Data;
using BackPeliculas.Resultados;
using BackPeliculas.Comandos.Personajes;
using BackPeliculas.Comandos.Peliculas;


namespace BackPeliculas.Controllers;

public class PeliculaController
{

    private readonly Context _context;
    public PeliculaController(Context context) { 
        _context = context; 

    }
    //---------------------------------------------------------------------------------------------------------
    [HttpGet]  //(7)
    [Route("/movies")]
    public async Task<ActionResult<ResPelicula>> GetMovies()
    {
        var result = new ResPelicula();

        var peliculas = await _context.Peliculas.ToListAsync();

        if(peliculas != null){

            foreach (var item in peliculas){
                var ResPelicItem = new ResPelicItemPlus{
                    PeliculaId = item.PeliculaId,
                    Imagen = item.Imagen,
                    Titulo = item.Titulo
                };
                result.listaPeliculas.Add(ResPelicItem);
            }
            result.StatusCode = 200;
        }
        else{
            result.StatusCode = 404;
            result.SetError("No se encuentran películas");
        }
        
        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpPost]  //(8)
    [Route("/movies/create")]
    public async Task<ActionResult<ResultadoBase>> CreatePelicula([FromBody] ComPelicula comando)
    {
        var result = new ResultadoBase();
        var idList = new List<Guid>();

        var genero = await _context.Generos.Where(x => x.Nombre.Equals(comando.Co_Genero_Nombre)).FirstOrDefaultAsync();
        
        if(genero != null)
        {          
            var personajes = await _context.Personajes.ToListAsync();
            int c = 0;
            foreach(var item1 in personajes){
                
                foreach(var item2 in comando.Co_listaPjesNombre){
                    if(item1.Nombre.Equals(item2)){
                        idList.Add(item1.PersonajeId);
                        c++;
                    }
                }

            }
            if(c == comando.Co_listaPjesNombre.Count){

                var pelicula = new Pelicula{
                    PeliculaId = Guid.NewGuid(),
                    Imagen = comando.Co_Imagen,
                    Titulo = comando.Co_Titulo,
                    FechaAlta = DateOnly.FromDateTime(DateTime.Now),
                    Calificacion = comando.Co_Calificacion
                };
                await _context.AddAsync(pelicula);

                foreach(var item in personajes){
                    foreach(var item2 in idList){
                        if(item.PersonajeId == item2 ){
                            var detPel = new DetallePelicula();
                            detPel.DetallePeliculaId = Guid.NewGuid();
                            detPel.IdPeliculaNavigationPeliculaId = pelicula.PeliculaId;
                            detPel.IdPersonajesNavigationPersonajeId = item2;
                            detPel.IdGeneroNavigationGeneroId = genero.GeneroId;

                            await _context.AddAsync(detPel);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                result.StatusCode = 200;
            }
            else{
                result.StatusCode = 404;
                result.SetError("Una o todas las Peliculas no existe");
            }
        }
        else
        {
            result.StatusCode = 404;
            result.SetError("Genero no existe");
        }



        return result;
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpPut]  //(8)
    [Route("/movies/update")]
    public async Task<ActionResult<ResultadoBase>> UpdatePelicula([FromBody] ComPeliculaId comando)
    {
        var result = new ResultadoBase();
        var pelicula = await _context.Peliculas.Where(p => p.PeliculaId == comando.Co_IdPelicula).FirstOrDefaultAsync();

        if(pelicula != null){
            pelicula.Imagen = comando.Co_Imagen;
            pelicula.Titulo = comando.Co_Titulo;
            pelicula.Calificacion = comando.Co_Calificacion;

            _context.Update(pelicula);
            await _context.SaveChangesAsync();
            
            result.StatusCode = 200;
        }
        else{
            result.StatusCode = 404;
            result.SetError("No existe pelicula");
        }
        
        return result;
    
    }
    //---------------------------------------------------------------------------------------------------------
    [HttpDelete]  //(8)
    [Route("/movies/delete/{idParam}")]
    public async Task<ActionResult<ResultadoBase>> DeletePelicula(Guid idParam)
    {
        var result = new ResultadoBase();

        var pelicula = await _context.Peliculas.Where(p => p.PeliculaId == idParam).FirstOrDefaultAsync();
        var detPeliculas = await _context.DetallePeliculas.Where(d => d.IdPeliculaNavigationPeliculaId == idParam).ToListAsync();
        
        if(pelicula != null && detPeliculas != null){
            _context.Remove(pelicula);
            foreach(var item in detPeliculas){
                _context.Remove(item);
            }
            await _context.SaveChangesAsync();
            result.StatusCode = 200;
        }
        else{
            result.StatusCode = 404;
            result.SetError("No existe pelicula");
        }
      
        return result;
    }
    //---------------------------------------------------------------------------------------------------------
}
