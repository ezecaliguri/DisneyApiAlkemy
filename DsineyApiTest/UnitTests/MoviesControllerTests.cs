using DisneyApi.Controllers;
using DisneyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DsineyApiTest.UnitTests
{
    [TestClass]
    public class MoviesControllerTests : ContextBase
    {
        [TestMethod]

        // GET Movies/name
        public async Task GetMoviesByNameTest_ResultTrue()
        {
            //preparacion

            var nameDb = Guid.NewGuid().ToString(); // nombre unico y aleatorio

            var context = BuildContext(nameDb);


            Movie movies = new() { CreationDate = DateTime.Now, Image = "url", Title = "pelicula1", Qualification = 10 };
            Movie movies2 = new() { CreationDate = DateTime.Now, Image = "url", Title = "pelicula2", Qualification = 7 };

            context.Movies.Add(movies);
            context.Movies.Add(movies2);
            context.SaveChanges();

            // instanciando otro contexto, para asegurar que no hay datos en memoria

            var context2 = BuildContext(nameDb);

            // prueba

            var controller = new MoviesController(context2);
            var nombre = "pelicula1";
            IActionResult actionResult = await controller.GetMovieName(nombre);            
            OkObjectResult result = actionResult as OkObjectResult;
            var model = result.Value as Movie;

            // resultado
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(nombre, model.Title);
        }




        [TestMethod]
        public async Task GetMoviesByGendersTest_ResultTrue()
        {
            // preparacion

            var nameDb = Guid.NewGuid().ToString();
            var context =  LoadData(nameDb);
            var id = 1;

            // prueba

            var controller = new MoviesController(context); 
            var actionResult = await controller.GetMoviesByGenders(id);
            OkObjectResult result = actionResult as OkObjectResult;

            // resultado

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }


        [TestMethod]
        public async Task GetMoviesByGendersTest_ResultFalse()
        {
            // preparacion

            var nameDb = Guid.NewGuid().ToString();
            var context = LoadData(nameDb);
            
            var id = 10;

            // prueba

            var controller = new MoviesController(context);
            var actionResult = await controller.GetMoviesByGenders(id);

            BadRequestObjectResult result = actionResult as BadRequestObjectResult;

            // resultado
            Assert.AreEqual(400, result.StatusCode);
        }


        // Busca 

        [TestMethod]

        public async Task GetMoviesTest_ResultOk()
        {
            // Preparacion 
            var nameDb = Guid.NewGuid().ToString(); // nombre unico y aleatorio

            var context = BuildContext(nameDb);


            Movie movies = new() { CreationDate = DateTime.Parse("2007-05-03"), Image = "url", Title = "pelicula1", Qualification = 10 };
            Movie movies2 = new() { CreationDate = DateTime.Now, Image = "url", Title = "pelicula2", Qualification = 7 };

            context.Movies.Add(movies);
            context.Movies.Add(movies2);
            context.SaveChanges();

            var context2 = BuildContext(nameDb);

            // prueba

            var controller = new MoviesController(context2);
            var actionResult = await controller.GetMovie();


            // Envio 5 parametros como prueba, 3 son los que recibo y los otros 2 deduzco que son del ActionResult, no pude ver las propiedades que son
            // Probe cambiando el retorno del Task<IactionResult> por un tipo Task<Object> para ver que me devolvia el tipo anonimo y me retornaba las 3 propiedades

            Assert.AreEqual(5, actionResult.GetType().GetProperties().Length);

        }


    }
}
