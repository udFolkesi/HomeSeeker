using BLL.Services;
using BLL.Services.Abstractions;
using Core.Entities;
using HomeSeeker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HomeSeeker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Announcement> _announcementService;

        public HomeController(ILogger<HomeController> logger, IService<Announcement> announcementService)
        {
            _logger = logger;
            _announcementService = announcementService;
        }

        public async Task<IActionResult> Index()
        {
            var properties = new List<HomeModel>
            {
                new HomeModel { Id = 1, Title = "Уютная квартира в центре", City = "Киев", Type = "Квартира", Price = 50000, ImageUrl = "/images/flat1.jpg" },
                new HomeModel { Id = 2, Title = "Просторный дом с садом", City = "Львов", Type = "Дом", Price = 120000, ImageUrl = "/images/logo.png" }
            };

            var announcementResponse = await _announcementService.GetAllAsync();
            var announcements = announcementResponse.Data;

            var model = new HomeModel
            {
                PopularAnnouncements = announcementResponse.Data,/*.Where(a => a.IsPopular)
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(5)
                    .ToList(),*/
                AnnouncementsForUser = announcementResponse.Data.ToList()
            };

            // View(properties);
            var model2 = new HomeModel
            {
                PopularAds = announcements.Select(p => new AnnouncementViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    City = p.Description.Address,
                    Category = p.Category.Name,
                    Price = p.Description.Price,
                    MainImage = p.Images.Where(p => p.IsMain).FirstOrDefault()?.Url
                    /*                    Category = p.Type,
                                        Price = p.Price,
                                        MainImage = p.ImageUrl*/
                }).ToList(),
                RecommendedAds = announcements.Select(p => new AnnouncementViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    City = p.Description.Address,
                    Category = p.Category.Name,
                    Price = p.Description.Price,
                    MainImage = p.Images.Where(p => p.IsMain).FirstOrDefault()?.Url
/*                    Id = p.Id,
                    Title = p.Title,
                    City = p.City,
                    Category = p.Type,
                    Price = p.Price,
                    MainImage = p.ImageUrl*/
                }).ToList()
            };
            return View(model2);
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