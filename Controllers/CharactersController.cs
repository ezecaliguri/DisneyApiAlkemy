using DisneyApi.Data;
using DisneyApi.Models;
using DisneyApi.Querys.Characters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DisneyApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CharactersController : ControllerBase
    {
        public readonly DisneyConnection _context;

        public CharactersController(DisneyConnection context)
        {
            _context = context;
        }


        //    ------------------------ GET Names Images ------------------------------

        [HttpGet]
        public async Task<IActionResult> GetCharactersBy()
        {
            var result = await _context.Characters.Select(x => new { x.Name, x.Image }).ToListAsync();

            if (result.Count == 0)
            {
                return BadRequest("Error en la carga");
            }
            return Ok(result);
        }
        
        //    ------------------------ GET Details ------------------------------

        [HttpGet]
        [Route("Details")]

        public async Task<IActionResult> GetCharactersDetails()
        {
            var details = await _context.Characters
                                        .Select(character => new
                                                {
                                                    character,
                                                    Movies = character.MovieCharacters.Select(mc => mc.Movie).ToList(),
                                                })
                                        .ToListAsync();

            if (details.Count == 0)
            {
                return BadRequest("Error en la carga de detalles");
            }
            return Ok(details);
        }


        //    ------------------------ GET Search By Name ------------------------------

        [HttpGet]
        [Route("name")]

        public async Task<IActionResult> GetCharacterByName([FromQuery] string name)
        {
            var byName = await _context.Characters.Where(x => x.Name == name).FirstOrDefaultAsync();

            if (byName == null)
            {
                return BadRequest("la pelicula no se encontro en la base de datos");
            }
            return Ok(byName);
        }

        //    ------------------------ GET Search By Age ------------------------------

        [HttpGet]
        [Route("Age")]
        public async Task<IActionResult> GetCharacterByAge([FromQuery] int age)
        {
            var byAge = await _context.Characters.Where(x => x.Age == age).ToListAsync();
            if (byAge.Count == 0)
            {
                return BadRequest("no se encontraron personajes correspondientes a esa edad");
            }
            return Ok(byAge);
        }

        //    ------------------------ GET Search By Movie ------------------------------

        [HttpGet]
        [Route("movies")]
        public async Task<IActionResult> GetByIdMovie([FromQuery] int idMov)
        {
            var resultado = await _context.Movies
                                          .Where(m => m.Id == idMov)
                                          .Select(Pelicula => new
                                          {
                                              Pelicula,
                                              Personajes = Pelicula.MovieCharacters.Select(c => c.Character).ToList()
                                          }).FirstOrDefaultAsync();
        
            if (resultado == null)
            {
                return BadRequest("No se encontraron personajes relacionados a esa pelicula, ¿Esta bien el id?");
            }
            return Ok(resultado);
        }




        //               ------------------------ POST ------------------------------

        // Created Character

        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CreateCharacter model)
        {
            var ExistCharacter = await ChekedCharacter(model.Name);

            if (ExistCharacter == true)
            {
                return BadRequest("El personaje ya existe");
            }

            var ChequedIds = await ChekedId(model.IdMovie);

            if(ChequedIds == false)
            {
                return BadRequest("El id de la pelicula no existe");
            }
            Character character = new()
            {
                Age = model.Age,
                Name = model.Name,
                History = model.History,
                Weight = model.Weight,
                Image = model.Image
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            var idchar = await _context.Characters.Where(c => c.Name == model.Name).Select(i => i.Id).FirstOrDefaultAsync();
            await SetMovieCharacters(idchar, model.IdMovie);

            return Ok("El personaje fue creado con exito");
        }

        private async Task<bool> ChekedId(int id)
        {
            var result = await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
            return result != null; // si existe devuelve true
        }
        private async Task<bool> SetMovieCharacters(int idCharacter, int idmovie)
        {

            MovieCharacter newRelation = new()
            {
                CharacterId = idCharacter,
                MovieId = idmovie
            };

            _context.MovieCharacters.Add(newRelation);
            await _context.SaveChangesAsync();

            return true;


        }


        // Post Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return BadRequest("No se encontro el personaje");
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return Ok("El personaje fue eliminado con exito");
        }


        // Post Update by Id

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCharacter(UpdateCharacter model, int id)
        {
            Character character = GetCharacter(id);
            if (character == null)
            {
                return BadRequest("No se encontro el personaje");
            }

            character.Age = model.Age;
            character.Name = model.Name;
            character.History = model.History;
            character.Weight = model.Weight;
            character.Image = model.Image;

            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Se realizaron los cambios con exito");
        }

        protected async Task<bool> ChekedCharacter(string name)
        {
            var result = await _context.Characters.Where(c => c.Name == name).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;
        }


        protected Character GetCharacter(int id)
        {
            var character = _context.Characters.Where(c => c.Id == id).First();
            return character;
        }
    }
}
