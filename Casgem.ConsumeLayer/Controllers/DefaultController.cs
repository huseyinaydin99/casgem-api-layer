using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Casgem.DtoLayer.DTOs;
using AutoMapper;
using Casgem.DtoLayer.DTOs.EstateDTOs;
using System.IO;

namespace Casgem.ConsumeLayer.Controllers
{
    public class DefaultController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public DefaultController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchForEverything = "", int price = 0, int room = 0)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7160/api/Estate");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<Casgem.DtoLayer.DTOs.EstateDTOs.Estate>>(jsonData);

                if (!string.IsNullOrEmpty(searchForEverything))
                {
                    var searchString = searchForEverything.ToLower();
                    values = values.Where(y =>
                        y.City.ToLower().Contains(searchString) ||
                        y.Title.ToLower().Contains(searchString) ||
                        y.Type.ToLower().Contains(searchString)).ToList();
                }

                if (price != 0 && room != 0)
                {
                    values = values.Where(y => y.Price <= price && y.Room == room).ToList();
                }
                else if (price != 0)
                {
                    values = values.Where(y => y.Price == price).ToList();
                }
                else if (room != 0)
                {
                    values = values.Where(y => y.Room == room).ToList();
                }
                else
                {
                    return View(values);
                }
                string createText = "Hello and Welcome" + Environment.NewLine;
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Huseyin_Aydin\\Desktop\\text_abc.txt"))
                {
                    writer.WriteLine("If'e girdi. Sorun yok.");
                }
                return View(values);
            }
            using (StreamWriter writer = new StreamWriter("C:\\Users\\Huseyin_Aydin\\Desktop\\text_abc.txt"))
            {
                writer.WriteLine("If'e girmedi.. Sorun var.");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Result(string id = null)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7160/api/Estate/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Estate>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}