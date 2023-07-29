using AutoMapper;
using Casgem.ConsumeLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Casgem.ConsumeLayer.Controllers
{
    public class EstateDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public EstateDetailController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string imageUrl, string Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7160/api/Estate/{Id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ShowEstateModel>(jsonData);
                //var modelList = _mapper.Map<List<ShowEstateModel>>(values);
                values.ImageUrl = imageUrl;
                return View(values);
            }
            return View();
        }
    }
}