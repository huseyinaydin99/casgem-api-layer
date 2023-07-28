using Casgem.ConsumeLayer.DTOs.LoginDTOs;
using Casgem.ConsumeLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Casgem.ConsumeLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7160/api/Login/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseJsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultLoginDto>(responseJsonData);

                HttpContext.Session.Clear();
                HttpContext.Session.SetString("username", values.Username);
                /**LoginViewModel viewModel = new LoginViewModel()
                {
                    Name = values.Name,
                    Surname = values.Surname,
                    Username = values.Username,
                    Password = values.Password,
                };*/
                model.Name = values.Name;
                model.Surname = values.Surname;
                model.UserName = values.Username;
                model.Password = values.Password;
                ViewBag.model = model;
                //return View(model);
                return RedirectToAction("Index", "Category");
            }
            model.Name = null;
            model.Surname = null;
            model.UserName = null;
            model.Password = null;
            model.ErrorMessage = "Kullanıcı adı veya şifre hatalı!";
            ViewBag.model = model;
            HttpContext.Session.SetString("username", model.ErrorMessage);
            HttpContext.Session.Clear();
            return View(model);
        }
    }
}