using Casgem.ConsumeLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Casgem.ConsumeLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index","Default");
        }

        public IActionResult Privacy()
        {
            return RedirectToAction("Index", "Default");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return RedirectToAction("Index", "Default");
        }
    }
}