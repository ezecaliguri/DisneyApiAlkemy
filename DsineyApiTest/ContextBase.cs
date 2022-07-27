using DisneyApi.Data;
using DisneyApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsineyApiTest
{
    public class ContextBase
    {

        //  Constructor de la Base de datos para Pruebas

       protected static DisneyConnection BuildContext(string nameDb)
        {
            var options = new DbContextOptionsBuilder<DisneyConnection>()
                .UseInMemoryDatabase(nameDb).Options;
            
            var dbContext = new DisneyConnection(options);
            return dbContext; 
        }



        //  Constructor de la Base de datos para Pruebas con carga previa

        protected static DisneyConnection LoadData(string nameDb)
        {
            var options = new DbContextOptionsBuilder<DisneyConnection>()
                .UseInMemoryDatabase(nameDb).Options;

            var context = new DisneyConnection(options);

            Movie movies = new() { CreationDate = DateTime.Now, Image = "url", Title = "pelicula1", Qualification = 10 };
            Movie movies2 = new() { CreationDate = DateTime.Now, Image = "url", Title = "pelicula2", Qualification = 7 };

            context.Movies.Add(movies);
            context.Movies.Add(movies2);

            Gender gender = new() { Name = "Fantasia", Image = "Url de la imagen" };
            Gender gender2 = new() { Name = "Accion", Image = "Url de la imagen" };

            context.Genders.Add(gender);
            context.Genders.Add(gender2);

            Character character = new() { Image = "Url de la imagen", Name = "Hombre Araña", Age = 23, History = "Historia", Weight = 60 };
            Character character2 = new() { Image = "Url de la imagen", Name = "Hulk", Age = 23, History = "Historia", Weight = 40 };

            context.Characters.Add(character2);
            context.Characters.Add(character);

            MovieCharacter movieCharacter = new() { CharacterId = 1, MovieId = 1 };
            MovieCharacter movieCharacter2 = new() { CharacterId = 1, MovieId = 2 };
            MovieCharacter movieCharacter3 = new() { CharacterId = 2, MovieId = 2 };

            context.MovieCharacters.Add(movieCharacter);
            context.MovieCharacters.Add(movieCharacter2);
            context.MovieCharacters.Add(movieCharacter3);

            MovieGender moviegender = new() { GenderId = 1, MovieId = 2 };
            MovieGender moviegender2 = new() { GenderId = 1, MovieId = 1 };
            MovieGender moviegender3 = new() { GenderId = 2, MovieId = 1 };

            context.MovieGenders.Add(moviegender);
            context.MovieGenders.Add(moviegender3);
            context.MovieGenders.Add(moviegender2);

            context.SaveChanges();

            return context;
        }
    }
}
