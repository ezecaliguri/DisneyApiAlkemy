using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyApi.Data;
using DisneyApi.Models;
using DisneyApi.Querys.Movies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace DisneyApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class MoviesController : ControllerBase
    {
        private readonly DisneyConnection _context;

        public MoviesController(DisneyConnection context)
        {
            _context = context;
        }

        //         ------------------------ GET ------------------------ 



        // Route: /Movies

        [HttpGet]
        public async Task<IActionResult> GetMovie()
        {
            var result = await _context.Movies.Select(x => new { x.Image, x.Title, x.CreationDate}).ToListAsync();

            if (result.Count == 0)
            {
                return NotFound("Error en la carga de datos");
            }


            return Ok(result);
        }



        // Route /Movie/Details

        [HttpGet]
        [Route("Details")]

        public async Task<IActionResult> GetDetailsMovies()
        {
            var result = await _context.Movies.Select(movie => new
                                        {
                                            movie,
                                            Personajes = movie.MovieCharacters.Select(mc => mc.Character).ToList()
                                        }).ToListAsync();
            if (result.Count == 0)
            {
                return NotFound("Error en la carga de datos");
            }

            return Ok(result);
        }



        // Route /Movie/name

        [HttpGet]
        [Route("name")]

        public async Task<IActionResult> GetMovieName([FromQuery] string name)
        {
            if(name == null)
            {
                return BadRequest("ingrese un nombre");
            }

            var result = await _context.Movies.Where(m => m.Title == name).FirstAsync();

            if (result == null) {
                return BadRequest("No se encontro una pelicula con ese nombre");
            }

            return Ok(result);
        }


        // Route /Movie/genre

        [HttpGet]
        [Route("genre")]

        public async Task<IActionResult> GetMoviesByGenders([FromQuery] int genre)
        {
            var result = await _context.Genders
                                          .Where(g => g.Id == genre)    
                                          .Select(Genders => new{
                                              Genders,
                                              Peliculas = Genders.MovieGenders.Select(g => g.Movie).ToList()
                                                  })
                                          .FirstOrDefaultAsync();
            if (result == null)
            {
                return BadRequest("No existe registro con ese Id");   
            }
            return Ok(result);
        }

        // Route /Movie/Order
        [HttpGet]
        [Route("order")]
        public async Task<IActionResult> GetMovieAscDesc([FromQuery] string order)
        {

            if(order != "asc" && order != "desc" )
            {
                return BadRequest("Escribir asc para ordenar ascendente o desc para ordenar descendente ");
            }
            var result = await GetAscDesc(order);
            if (result.Count == 0)
            {
                BadRequest("Error al cargar");
            }

            return Ok(result);

        }

        protected async Task<List<Movie>> GetAscDesc(string date)
        {

            if(date == "asc")
            {
                 var result = await _context.Movies.OrderBy(m => m.CreationDate).ToListAsync();
                    return result;
            } else 
            {
                 var result = await _context.Movies.OrderByDescending(m => m.CreationDate).ToListAsync();
                return result;
            }
        }


        protected async Task<bool> CheckedMovie(string title)
        {
            var result = await _context.Movies.Where(m => m.Title == title).FirstOrDefaultAsync();
            return result != null;
        }

        protected Movie GetMovie(int id)
        {
            var movie =  _context.Movies.Where(m => m.Id == id).First();
            return movie;
        }

        //         ------------------------ POST ------------------------ 



        //Post Created

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(CreateMovie movie)
        {
            var ExistMovie = await CheckedMovie(movie.Title);

            if (ExistMovie != false)
            {
                return BadRequest("La pelicula ya existe");
            }

            var chekedId = await ChekedId(movie.IdCharacter, movie.IdGender);
            if(chekedId == false)
            {
                return BadRequest("El id del personaje o del genero no existe");
            }

            Movie movieNew = new()
            {
                Title = movie.Title,
                Qualification = movie.Qualification,
                CreationDate = movie.CreationDate,
                Image = movie.Image
            };

            _context.Movies.Add(movieNew);
            await _context.SaveChangesAsync();

            var idmov = await _context.Movies.Where(m => m.CreationDate == movie.CreationDate).Select(i => i.Id).FirstOrDefaultAsync();

            await SetMovieCharacters(movie.IdCharacter, idmov);
            await SetMovieGenders(movie.IdGender, idmov);

            return Ok("Se creo la pelicula con exito");
        }


        private async Task<bool> ChekedId(int idCharacter, int idGender)
        {
            var result1 = await _context.Characters.Where(c => c.Id == idCharacter).FirstOrDefaultAsync();
            var result2 = await _context.Genders.Where(g => g.Id == idGender).FirstOrDefaultAsync();
            if(result1 == null)
            {
                return false;
            } else if (result2 == null)
            {
                return false;
            }
            return true;
        }
        private async Task SetMovieCharacters(int idCharacter, int idmovie)
        {
            MovieCharacter movieCharacter = new ()
            { 
                CharacterId = idCharacter, 
                MovieId = idmovie 
            };
             _context.MovieCharacters.Add(movieCharacter);            
            await _context.SaveChangesAsync();

        }


        private async Task SetMovieGenders(int idgenders, int idmovie)
        {
            MovieGender movieGender = new()
            {
                GenderId = idgenders,
                MovieId = idmovie
            };
            _context.MovieGenders.Add(movieGender);
            await _context.SaveChangesAsync();

        }


        // Post Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return BadRequest("No se encontro la pelicula");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok("La pelicula fue eliminada con exito");
        }


        // Post Update by Id

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateMovie(UpdateMovie mov, int id)
        {
            Movie movie = GetMovie(id);
            if (movie == null)
            {
                return BadRequest("No se encontro la pelicula");
            }

            movie.Title = mov.Title;
            movie.Qualification = mov.Qualification;
            movie.CreationDate = mov.CreationDate;
            movie.Image = mov.Image;

            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Se realizaron los cambios con exito");
        }

    }
}

