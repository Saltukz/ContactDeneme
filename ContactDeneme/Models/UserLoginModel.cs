using System.ComponentModel.DataAnnotations;

namespace ContactDeneme.Models
{
    public class UserLoginModel
    {

        [Required]
        public string Username { get; set; } = null!;


        [Required]
        public string Password { get; set; } = null!;
    }
}
