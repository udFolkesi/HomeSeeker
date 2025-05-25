using BLL.Services;
using BLL.Services.Abstractions;
using Core.Entities;
using Core.Models;
using HomeSeeker.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeSeeker.Controllers
{
    public class AccountController: Controller
    {
        private readonly IAccountService _accountService;
        private readonly IService<User> _userService;
        //private readonly IService<Course> _courseService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IService<User> userService, ILogger<AccountController> logger, IAccountService accountService)
        {
            //_courseService = courseService;
            _userService = userService;
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new();

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);


                //if (response.StatusCode == Core.Enums.StatusCode.Ok)
                //{
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                //}

                //ModelState.AddModelError("", response.Info);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);


/*                if (responce.StatusCode == Core.Enums.StatusCode.Ok)
                {*/
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
/*                }

                return RedirectToAction("Index", "Home");*/
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile(int id = 0)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == 0)
            {
                id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            }

            var response = await _userService.GetByIdAsync(id);
            //var responceCourses = await _courseService.GetAll();
            //var authorCourses = responceCourses.Data.Where(c => c.AuthorId == response.Data.Id).ToList();
            _logger.LogInformation("View User -> Ok");

            var model = new ProfileViewModel() { Data = response.Data };

            return View(model);
        }
    }
}
