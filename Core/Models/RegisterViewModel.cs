using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 characters!")]
        [MinLength(3, ErrorMessage = "Min 4 characters!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Min 7 characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not confirmed")]
        public string ConfirmPassword { get; set; }
    }
}
