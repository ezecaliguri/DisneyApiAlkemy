using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyApi.Data;
using DisneyApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DisneyApi.Querys.Genders;

namespace DisneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class GendersController : ControllerBase
    {
        private readonly DisneyConnection _context;

        public GendersController(DisneyConnection context)
        {
            _context = context;
        }
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var result = await _context.Genders.ToListAsync();
            if(result.Count == 0)
            {
                return BadRequest("Error en la carga");
            }
            return Ok(result);
        }

        // POST

        [HttpPost]
        public async Task<IActionResult> PostGender(CreateGender model)
        {
            var chekedGender = await ExistedGender(model.Name);
            if (chekedGender == true)
            {
                return NotFound("El Genero ya existe");
            }

            Gender gender = new()
            {
                Name = model.Name,
                Image = model.Image
            };

            var result =  _context.Genders.Add(gender);
            await _context.SaveChangesAsync();

            if(result == null)
            {
                return BadRequest("Error en la creacion del genero");
            }

            return Ok(gender);
        }

        private async Task<bool> ExistedGender(string name)
        {
            var result  = await _context.Genders.Where(g => g.Name == name).FirstOrDefaultAsync();
            return result != null;
        }
        private async Task<bool> ExistedGender(int id)
        {
            var result = await _context.Genders.Where(g => g.Id == id).FirstOrDefaultAsync();
            return result != null;

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutGender(CreateGender model, int id)
        {
            var ExistGender = await ExistedGender(id);
            if (ExistGender == false)
            {
                return BadRequest("El genero no existe");

            }
            Gender gender = GetGender(id);

            gender.Name = model.Name;
            gender.Image = model.Image;
            
            
            _context.Entry(gender).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Se realizaron los cambios con exito");

        }


        private Gender GetGender(int id)
        {
            return  _context.Genders.Where(g => g.Id == id).First();
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteGender(int id)
        {
            var ExistGender = await ExistedGender(id);
            if (ExistGender == false)
            {
                return BadRequest("Id incorrecto");

            }
            var gender = GetGender(id);

            _context.Genders.Remove(gender);
            await _context.SaveChangesAsync();
            return Ok("Genero borrado");
        }
    }  
}

