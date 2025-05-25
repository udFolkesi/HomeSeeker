using HomeSeeker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeSeeker.Controllers
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
            var properties = new List<HomeModel>
            {
                new HomeModel { Id = 1, Title = "Уютная квартира в центре", City = "Киев", Type = "Квартира", Price = 50000, ImageUrl = "/images/flat1.jpg" },
                new HomeModel { Id = 2, Title = "Просторный дом с садом", City = "Львов", Type = "Дом", Price = 120000, ImageUrl = "/images/house1.jpg" }
            };

            return View(properties);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}