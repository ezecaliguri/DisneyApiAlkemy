using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Querys.Movies
{
    public class UpdateMovie
    {
        [Required]
        [StringLength(100, ErrorMessage = "longitud maxima 100 caracteres")]
        [MinLength(1, ErrorMessage = "la longitud minima es de 1 caracter")]
        public string Image { get; set; }


        [Required(ErrorMessage = "Requiere la fecha")]
        public DateTime CreationDate { get; set; }


        [Required, StringLength(50, ErrorMessage = "longitud maxima 50 caracteres")]
        [MinLength(1, ErrorMessage = "la longitud minima es de 1 caracter")]
        public string Title { get; set; }



        [Required(ErrorMessage = "Calificacion requerida")]
        public int Qualification { get; set; }


        //[Required(ErrorMessage = "colocar por lo menos el id de  1 personaje")]
        //public int IdCharacter { get; set; }

        //[Required(ErrorMessage = "colocar por lo menos el id 1 personaje")]

        //public int IdGender { get; set; }
    }
}
