using BLL.Services.Abstractions;
using Core.Entities;
using HomeSeeker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace HomeSeeker.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ILogger<AnnouncementController> _logger;
        private readonly IService<Announcement> _announcementService;

        public AnnouncementController(IService<Announcement> announcementService, ILogger<AnnouncementController> logger)
        {
            _announcementService = announcementService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> View(int id)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _announcementService.GetByIdAsync(id);

/*            if (response.Data.Status == "Active")
            {
                if (HttpContext.User.FindFirst("AnnouncementsIds").Value.Split(',').Contains(response.Data.Id.ToString()))
                {
                    _logger.LogInformation("View Announcement -> Ok");
                    return View(response.Data);
                }
                else
                {
                    var responceAnnouncements = await _announcementService.GetAllAsync();
                    var announcement = responceAnnouncements.Data.FirstOrDefault(c => c.Id == response.Data.Id);

                    if (announcement is not null)
                    {
                        _logger.LogInformation("View Announcement -> Ok");
                        return View(announcement);
                    }
                }
                return RedirectToAction("Index", "Home");
            }*/

            _logger.LogInformation("View Course -> Ok");
            return View(response.Data);
        }

        public async Task<IActionResult> ViewAll(string search, string city, int? priceFrom, int? priceTo, string sort)
        {
            var announcementsResponse = await _announcementService.GetAllAsync();
            var ads = announcementsResponse.Data.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                ads = ads.Where(a => a.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(city))
                ads = ads.Where(a => a.Description.Address.Contains(city, StringComparison.OrdinalIgnoreCase));

            if (priceFrom.HasValue)
                ads = ads.Where(a => a.Description.Price >= priceFrom.Value);

            if (priceTo.HasValue)
                ads = ads.Where(a => a.Description.Price <= priceTo.Value);

            // Сортування
            ads = sort switch
            {
                "price_asc" => ads.OrderBy(a => a.Description.Price),
                "price_desc" => ads.OrderByDescending(a => a.Description.Price),
                _ => ads // За замовчуванням — без сортування (можна реалізувати за релевантністю)
            };

            var announcementViewModels = new List<AnnouncementViewModel>();

            foreach (var ad in ads)
            {
                var timeToCenter = await GetTimeToCenter(ad.Description.Latitude, ad.Description.Longitude, ad.Description.Address);
                int time = Convert.ToInt32(timeToCenter ?? 0);

                announcementViewModels.Add(new AnnouncementViewModel
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    City = ad.Description.Address,
                    Category = ad.Category?.Name,
                    Price = ad.Description.Price,
                    MainImage = ad.Images.FirstOrDefault(p => p.IsMain)?.Url,
                    TimeToCenter = time
                });
            }

            var announcementModel = new AnnouncementModel
            {
                Search = search,
                City = city,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                Sort = sort,
                Ads = announcementViewModels
            };

            return View("ViewAll", announcementModel);
        }


        private async Task<double?> GetTimeToCenter(double? adLat, double? adLon, string city)
        {
            if (adLat == null || adLon == null || string.IsNullOrWhiteSpace(city))
            {
                return null;
            }

            string apiKey = "5b3ce3597851110001cf62488891a1f132464b22ba6d05816fdc907b";
            var http = new HttpClient();

            try
            {
                var geoUrl = $"https://api.openrouteservice.org/geocode/search?api_key={apiKey}&text={Uri.EscapeDataString(city)}";
                var geoResp = await http.GetStringAsync(geoUrl);
                var geoJson = JsonDocument.Parse(geoResp);

                var features = geoJson.RootElement.GetProperty("features");
                if (features.GetArrayLength() == 0)
                {
                    Console.WriteLine($"Geocoding failed for city: {city}");
                    return null;
                }

                var coords = features[0].GetProperty("geometry").GetProperty("coordinates");
                (double cityLon, double cityLat) = (coords[0].GetDouble(), coords[1].GetDouble());

                var body = @$"{{
                    ""locations"": [
                        [{adLon.Value.ToString(CultureInfo.InvariantCulture)}, {adLat.Value.ToString(CultureInfo.InvariantCulture)}],
                        [{cityLon.ToString(CultureInfo.InvariantCulture)}, {cityLat.ToString(CultureInfo.InvariantCulture)}]
                    ],
                    ""metrics"": [""duration""]
                }}";

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openrouteservice.org/v2/matrix/driving-car");
                request.Headers.Add("Authorization", apiKey);
                request.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");

                var response = await http.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var json = JsonDocument.Parse(responseString);
                    var seconds = json.RootElement.GetProperty("durations")[0][1].GetDouble();
                    return seconds / 60;
                }
                else
                {
                    Console.WriteLine($"Error fetching route: {response.StatusCode}, Response: {responseString}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred in GetTimeToCenter: {ex.Message}");
                return null;
            }
        }

        public IActionResult AddOrEdit()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var model = new Announcement()
                {
                    Description = new ObjectDescription(),
                    Category = new Category(),
                    //Profile = new Profile(),
                };
                var categories = new List<Category>
                {
                    new Category { Name = "Квартира", Description = "Житло в багатоквартирному будинку" },
                    new Category { Name = "Дім", Description = "Окремо розташований житловий будинок" },
                    new Category { Name = "Кімната", Description = "Окрема кімната в квартирі або гуртожитку" },
                    new Category { Name = "Земельна ділянка", Description = "Ділянка під забудову або сад" }
                };
                ViewBag.Categories = categories; // или любой другой способ получить список

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Announcement announcement)
        {

            /*            var userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
                        announcement.
            
            
            
            = userId;*/

            announcement.ProfileID = 3;//int.Parse(HttpContext.User.FindFirst("Id").Value);
            announcement.CategoryID = 4;
            ModelState.Remove("Description.Profile");
            ModelState.Remove("Category");
            ModelState.Remove("Profile");

            foreach (var entry in ModelState)
            {
                var key = entry.Key;
                var errors = entry.Value.Errors;

                foreach (var error in errors)
                {
                    _logger.LogError($"ModelState error in field '{key}': {error.ErrorMessage}");
                }
            }
            //announcement.Status = Core.Enums.AnnouncementStatus.Draft;
            /*            if (announcement.ImageFiles == null || !announcement.ImageFiles.Any())
                        {
                            ModelState.AddModelError("ImageFiles", "At least one image is required.");
                        }*/
            
            if (ModelState.IsValid)
            {
                announcement.ProfileID = int.Parse(HttpContext.User.FindFirst("Id").Value.ToString());

                var response = announcement.Id == 0
                    ? await _announcementService.AddAsync(announcement)
                    : await _announcementService.UpdateAsync(announcement);

                if (!response.Success)
                {
                    _logger.LogError("AddOrEdit Announcement -> Error");
                    return RedirectToAction("AddOrEdit", new { id = announcement.Id });
                }

                _logger.LogInformation("AddOrEdit Announcement -> Ok");
                return RedirectToAction("View", new { id = announcement.Id });
            }

            _logger.LogError("AddOrEdit Announcement -> ModelState Invalid");
            return View(announcement);
        }
    }
}
