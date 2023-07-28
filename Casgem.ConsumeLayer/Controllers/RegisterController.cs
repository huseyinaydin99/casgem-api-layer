using Casgem.DtoLayer.DTOs.RegisterDTOs;
using Casgem.ConsumeLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace Casgem.ConsumeLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserModel appUserModel)
        {
            
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(appUserModel);
            
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7160/api/Register/", stringContent);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseJsonData = await responseMessage.Content.ReadAsStringAsync();
                
                var values = JsonConvert.DeserializeObject<AppUserDto>(responseJsonData);

                HttpContext.Session.Clear();
                HttpContext.Session.SetString("username", appUserModel.UserName);
                LoginViewModel viewModel = new LoginViewModel()
                {
                    Name = appUserModel.Name,
                    Surname = appUserModel.Surname,
                    UserName = appUserModel.UserName,
                    Password = appUserModel.Password,
                };
                /*
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Huseyin_Aydin\\Desktop\\test_abc.txt"))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(values).ToString());
                    writer.WriteLine("Selam");
                    writer.Flush();
                    writer.Close();
                }
                */
                ViewBag.model = appUserModel;/*
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Huseyin_Aydin\\Desktop\\test_abc.txt"))
                {
                    writer.WriteLine("Register başarılı. Model Tamam!");
                }**/
                return RedirectToAction("Index", "Default");
            }
            appUserModel.Name = null;
            appUserModel.Surname = null;
            appUserModel.UserName = null;
            appUserModel.PasswordHash = null;
            ViewBag.model = appUserModel;
            HttpContext.Session.SetString("username", "İşlem başarısız.");
            HttpContext.Session.Clear();
            /*using (StreamWriter writer = new StreamWriter("C:\\Users\\Huseyin_Aydin\\Desktop\\test_abc.txt"))
            {
                writer.WriteLine("Register başarısız. Patladı");
            }*/
            return View(appUserModel);
        }
    }
}