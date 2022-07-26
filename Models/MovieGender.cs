using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisneyApi.Models
{
    public class MovieGender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int GenderId { get; set; }

        public Gender Gender { get; set; }
    }
}
