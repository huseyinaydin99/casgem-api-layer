using AutoMapper;
using Casgem.ConsumeLayer.DTOs.LoginDTOs;
using Casgem.ConsumeLayer.Models;
using Casgem.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Text;
using ThirdParty.Json.LitJson;

namespace Casgem.ConsumeLayer.Controllers
{
    public class EstateController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public EstateController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(user))
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7160/api/Estate/");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ShowEstateModel>>(jsonData);
                    //var modelList = _mapper.Map<List<ShowEstateModel>>(values);
                    return View(values);
                }
            }
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Index(AddEstateModel model)
        {
            
            
        }*/

        [HttpGet]
        public async Task<IActionResult> UpdateEstate(string id)
        {
            var user = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(user))
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync($"https://localhost:7160/api/Estate/{id}");
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Huseyin_Aydin\\Desktop\\textt_abc.txt"))
                {
                    writer.WriteLine(responseMessage.StatusCode);
                    writer.WriteLine("abcabcxxxx");
                    writer.Flush();
                    writer.Close();
                }
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<UpdateEstateModel>(jsonData);

                    //var modelList = _mapper.Map<List<ShowEstateModel>>(values);
                    return View(values);
                }
            }
            return RedirectToAction("Index","Default");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEstate(UpdateEstateModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7160/api/Estate/{model.Id}", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Estate");
            }
            else if (responseMessage.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Huseyin_Aydin\\Desktop\\not_found.txt"))
                {
                    writer.WriteLine("Böyle bir kayıt yokki!!!!!");
                    writer.WriteLine("qqqqq");
                    writer.Flush();
                    writer.Close();
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEstate(DeleteEstateModel model)
        {
            var estate = _mapper.Map<Estate>(model);
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.DeleteAsync($"https://localhost:7160/api/Estate/{model.Id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                /*
                var responseJsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<AddEstateModel>(responseJsonData);
                var estate_entity = _mapper.Map<Estate>(values);
                */

                /*
                model.Title = values.Title;
                model.Price = values.Price;
                model.City = values.City;
                model.Room = values.Room;
                */
                return RedirectToAction("Index", "Estate");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddEstate()
        {
            var user = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(user))
            {
                return View();
            }
            return RedirectToAction("Index","Default");
        }

        [HttpPost]
        public async Task<IActionResult> AddEstate(AddEstateModel model)
        {
            var estate = _mapper.Map<Estate>(model);
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(estate);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7160/api/Estate/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                /*var responseJsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<AddEstateModel>(responseJsonData);
                var estate_entity = _mapper.Map<Estate>(values);*/
                /*
                model.Title = values.Title;
                model.Price = values.Price;
                model.City = values.City;
                model.Room = values.Room;
                */
                return RedirectToAction("Index", "Estate");
            }
            return View();
        }
    }
}
