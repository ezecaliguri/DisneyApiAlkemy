using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DisneyApi.Models;

namespace DisneyApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Character>()
                .HasData(
                    new Character{ Id = 1 , Image = "Url de la imagen",Name = "Hombre Araña", Age = 23, History = "Es un personaje creado por los estadounidenses Stan Lee y Steve Ditko",
                                    Weight = 60},
                    new Character{ Id = 2 , Image = "Url de la imagen",Name = "Harry Potter", Age = 18, History = "Es el personaje principal de la saga Harry Potter",
                                    Weight = 74},
                    new Character{ Id = 3 , Image = "Url de la imagen",Name = "Iron Man", Age = 55, History = "es un superhéroe que aparece en los cómics estadounidenses publicados por Marvel Comics.",
                                    Weight = 120},
                    new Character{ Id = 4 , Image = "Url de la imagen",Name = "Hulk", Age = 23, History = "Hulk es un personaje ficticio, un superhéroe que aparece en los cómics estadounidenses publicados por la editorial Marvel Comics,",
                                    Weight = 200},
                    new Character{ Id = 5 , Image = "Url de la imagen",Name = "Hermione Granger", Age = 18, History = "Hermione Jean Granger es un personaje de ficción y una de los tres protagonistas principales de la serie de libros de Harry Potter, publicados por J. K.",
                                Weight = 80}
                    );


            modelBuilder.Entity<Gender>()
                .HasData(
                        new Gender{ Id = 1 , Name = "Fantasia", Image = "Url de la imagen" },
                        new Gender{ Id = 2 , Name = "Accion", Image = "Url de la imagen" },
                        new Gender{ Id = 3 , Name = "Drama", Image = "Url de la imagen" },
                        new Gender{ Id = 4 , Name = "Comedia", Image = "Url de la imagen" }                        
                    );

            modelBuilder.Entity<Movie>()
                .HasData(
                        new Movie{ Id = 1 , Image = "Url de la imagen", Title = "Spiderman", CreationDate = DateTime.Parse("2007-05-03"), Qualification = 8},
                        new Movie{ Id = 2 , Image = "Url de la imagen", Title = "Harry Potter 4", CreationDate = DateTime.Parse("2005-10-24"), Qualification = 9},
                        new Movie{ Id = 3 , Image = "Url de la imagen", Title = "Avengers", CreationDate = DateTime.Parse("2012-04-26"), Qualification = 6}
                        );

            modelBuilder.Entity<MovieCharacter>()
                .HasData(
                        new MovieCharacter{ Id = 1, CharacterId = 1, MovieId = 1},
                        new MovieCharacter{ Id = 2, CharacterId = 2, MovieId = 2},
                        new MovieCharacter{ Id = 3, CharacterId = 3, MovieId = 3},
                        new MovieCharacter{ Id = 4, CharacterId = 4, MovieId = 3},
                        new MovieCharacter{ Id = 5, CharacterId = 5, MovieId = 2},
                        new MovieCharacter{ Id = 6, CharacterId = 3, MovieId = 1 }

                        );

            modelBuilder.Entity<MovieGender>()
                .HasData(
                        new MovieGender { Id = 1, GenderId = 3, MovieId = 3 },
                        new MovieGender { Id = 2, GenderId = 2, MovieId = 1 },
                        new MovieGender { Id = 3, GenderId = 3, MovieId = 1 },
                        new MovieGender { Id = 4, GenderId = 1, MovieId = 2 },
                        new MovieGender { Id = 5, GenderId = 2, MovieId = 2 }
                        );



        }
    }
}
