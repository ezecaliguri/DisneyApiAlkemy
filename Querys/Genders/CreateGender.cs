using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Querys.Genders
{
    public class CreateGender
    {
        
        
        [Required, StringLength(100, ErrorMessage = "la longitud maxima para el url de la imagen es de 100 caracteres")]
        [MinLength(10, ErrorMessage = "la longitud minima para el url de la imagen es de 10 caracteres")]
        public string Image { get; set; }

        [Required, StringLength(50, ErrorMessage = "longitud maxima para el nombre es de 50 caracteres")]
        [MinLength(1, ErrorMessage = "la longitud minima para el nombre es de  1 caracter")]
        public string Name { get; set; }
    }
}
