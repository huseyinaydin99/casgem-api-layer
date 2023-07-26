using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Casgem.ApiLayer.DTOs
{
    public class RegisterRequest : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
        public int? ConfirmCode { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
