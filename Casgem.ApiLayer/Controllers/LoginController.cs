using Casgem.ApiLayer.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Casgem.EntityLayer.Concrete;

namespace Casgem.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            LoginInfoResponse response = new LoginInfoResponse()
            {
                Name = null,
                Surname = null,
                Username = null,
                Password = null,
                ErrorMessage = null
            };
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
            if (result.Succeeded)
            {
                response.Username = request.Username;
                response.Password = request.Password;
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("username", request.Username);
                response.ErrorMessage = null;
                return Ok(response);
            }

            response.ErrorMessage = "Kullanıcı adı veya şifre hatalı!";
            HttpContext.Session.SetString("username", response.ErrorMessage);
            HttpContext.Session.Clear();
            return Unauthorized(response);
        }
    }
}
