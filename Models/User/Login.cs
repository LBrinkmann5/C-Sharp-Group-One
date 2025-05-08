using System.ComponentModel.DataAnnotations;
namespace Southwest_Airlines.Models.User
{
    public class Login
    {
        [Required]
        public string TBuser { get; set; }
        [Required]
        public string TBpass { get; set; }

    }
}
