using Casgem.ApiLayer.DTOs;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
/*
using MailKit.Net.Smtp;
using MimeKit;
*/
using Casgem.EntityLayer.Concrete;
using Casgem.ApiLayer.Model;

namespace Casgem.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            /* if (ModelState.IsValid)
             {*/
            Random rnd = new Random();
            int x = rnd.Next(100000, 1000000);
            AppUser appUser = new AppUser()
            {
                Name = model.Name,
                Email = model.Email,
                Surname = model.Surname,
                UserName = model.UserName,
                City = model.City,
                ConfirmCode = x
            };
            if (model.Password == model.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("username", model.UserName);
                    //SendEmail(registerRequest, x);

                    //TempData["Username"] = appUser.UserName;
                    //TempData["User"] = appUser;

                    //return RedirectToAction("Index", "Confirm");
                    return Ok();
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return Unauthorized();
                }
            }
            else
            {
                //ModelState.AddModelError("", "Şifreler eşleşmiyor.");
            }
            return Unauthorized();
        }

        /*
        private static void SendEmail(RegisterViewModel model, int x)
        {
            #region
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddress = new MailboxAddress("Admin", "huseyinaydin99@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", model.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Giriş için onay kodunuz: " + x.ToString();
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Doğrulama kodu";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("huseyinaydin99@gmail.com", "tcizvcpzzcpfxxoy");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
            #endregion
        }
        */
    }
}
