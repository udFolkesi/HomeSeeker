using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HomeSeeker.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrEdit()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var model = new Announcement();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
