using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Querys.Characters
{
    public class UpdateCharacter
    {
        [Required, StringLength(100, ErrorMessage = "longitud maxima 100 caracteres")]
        [MinLength(1, ErrorMessage = "la longitud minima es de 1 caracter")]
        public string Image { get; set; }

        [Required, StringLength(50, ErrorMessage = "longitud maxima 50 caracteres")]
        [MinLength(1, ErrorMessage = "la longitud minima es de 1 caracter")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Edad requerida")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Peso requerida")]
        public int Weight { get; set; }


        [Required, MinLength(10, ErrorMessage = "la longitud minima es de 10 caracter")]
        public string History { get; set; }

        //[Required(ErrorMessage = "colocar por lo menos el id de 1 pelicula")]
        //public int IdMovie { get; set; }
    }
}
