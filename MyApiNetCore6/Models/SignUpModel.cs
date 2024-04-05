using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore6.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Nhập Ho đi")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Nhập tên đi")]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
