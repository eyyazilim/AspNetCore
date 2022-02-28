using EyIdentityApp.Entities;
using EyIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EyIdentityApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new UserRegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                };
                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded) return RedirectToAction("Index");
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
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

        public IActionResult SignIn()
        {
            return View(new UserSignInModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
                if (signInResult.Succeeded)
                {
                    //İşlem Başarılı
                    return RedirectToAction("Index");
                }
                else if (signInResult.IsLockedOut)
                {
                    //Hesap Kilitlidir.
                }
                else if (signInResult.IsNotAllowed)
                { 
                    //Email yada phonenember doğrulanmamış 
                }
            }
            return View(model);
        }
    }
}
