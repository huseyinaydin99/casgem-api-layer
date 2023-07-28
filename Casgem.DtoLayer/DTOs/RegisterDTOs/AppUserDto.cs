using Microsoft.AspNetCore.Identity;

namespace Casgem.DtoLayer.DTOs.RegisterDTOs
{
    public class AppUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        //[Required(ErrorMessage = "Email alanı boş geçilemez.")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string City { get; set; }

        //[Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
        public int? ConfirmCode { get; set; }
    }
}