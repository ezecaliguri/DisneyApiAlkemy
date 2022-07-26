using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Querys.Users
{
    public class LoginUser
    {

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
