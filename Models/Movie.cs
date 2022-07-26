using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisneyApi.Models
{
   
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]

        public string Image { get; set; }
        [StringLength(50)]

        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; } 

        public int Qualification { get; set; }

        public List<MovieCharacter> MovieCharacters { get; set; }
        public List<MovieGender> MovieGenders { get; set; }

        
    }
}
