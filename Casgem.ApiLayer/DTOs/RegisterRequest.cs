using System.ComponentModel.DataAnnotations;

namespace Casgem.ApiLayer.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Ad alanı boş geçilemez.")]
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email alanı boş geçilemez.")]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string City { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
