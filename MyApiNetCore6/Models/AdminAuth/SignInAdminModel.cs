using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore6.Models.AdminAuth
{
    public class SignInAdminModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
