using Core.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class User: BaseEntity
    {
        //[Required(ErrorMessage = "Login must have up to 30 character"), MaxLength(30)]
        [Required]
        [EmailAddress]
        [MaxLength(50, ErrorMessage = "Max 50 characters!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password must have at least 6 character, up to 30")]
        public string PasswordHash { get; set; }
        public Profile Profile { get; set; }
    }
}
