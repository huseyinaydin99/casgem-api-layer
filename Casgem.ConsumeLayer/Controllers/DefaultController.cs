﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Casgem.DtoLayer.DTOs;
using AutoMapper;
using Casgem.DtoLayer.DTOs.EstateDTOs;

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


        /*
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }*/

        [HttpGet]
        public async Task<IActionResult> Index(string p = "", int price = 0, int room = 0)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7160/api/Estate");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<Estate>>(jsonData);

                if (!string.IsNullOrEmpty(p))
                {
                    var searchString = p.ToLower();
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

                return View(values);
            }
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Search(string? city, string? type, int? room, string? title, int? price, string? buildYear)
        //{
        //    var queryString = $"?city={city}&type={type}&room={room}&title={title}&price={price}&buildYear={buildYear}";
        //    var client = _httpClientFactory.CreateClient();
        //    var responseMessage = await client.GetAsync($"https://localhost:7160/api/Estate/filter{queryString}");

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //        var values = JsonConvert.DeserializeObject<List<Estate>>(jsonData);
        //        return PartialView("_SearchResults", values);
        //    }

        //    return PartialView("_SearchResults", new List<Estate>());
        //}


        [HttpGet]
        public async Task<IActionResult> Result(string id = null)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44314/api/Estate/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Estate>(jsonData);
                return View(values);
            }
            return View();
        }
        

        /*
        [HttpGet]
        public async Task<IActionResult> Result(string id = null)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44314/api/Estate/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Estate>(jsonData);
                return View(values);
            }
            return View();
        }*/
    }
}