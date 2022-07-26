using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisneyApi.Models
{
    
    public class Gender
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Image { get; set; }


        public List<MovieGender> MovieGenders { get; set; }


    }
}
