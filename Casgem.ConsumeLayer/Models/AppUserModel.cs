using Microsoft.AspNetCore.Identity;

namespace Casgem.ConsumeLayer.Models
{
    public class AppUserModel : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
        public int? ConfirmCode { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}