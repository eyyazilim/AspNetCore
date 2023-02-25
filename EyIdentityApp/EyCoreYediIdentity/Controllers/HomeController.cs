using EyCoreYediIdentity.Entities;
using EyCoreYediIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace EyCoreYediIdentity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
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
                if (identityResult.Succeeded)
                {
                    var memberRole = await _roleManager.FindByNameAsync("Member");
                    if (memberRole == null)
                    {
                        await _roleManager.CreateAsync(new AppRole() { Name = "Member", CreatedTime = DateTime.Now });
                        await _userManager.AddToRoleAsync(user, "Member");
                    }

                    return RedirectToAction("Index");
                }
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
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl)
        {
            return View(new UserSignInModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RenemberMe, true);
                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl)) return Redirect(model.ReturnUrl);
                    //İşlem Başarılı

                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                        return RedirectToAction("AdminManager");
                    else
                        return RedirectToAction("MemberManager");
                }
                else if (signInResult.IsLockedOut)
                {
                    if (user != null)
                    {
                        var LockOutEnd = await _userManager.GetLockoutEndDateAsync(user);
                        message = $"Hesabınız askıya alınmıştır! {(LockOutEnd.Value.UtcDateTime - DateTime.UtcNow).Minutes} dk. sonra tekrar aktif edilecektir.";
                    }
                    else
                        message = "Hesabınız geçici olarak kilitlenmiştir!";
                }
                else
                {
                    if (user != null)
                    {
                        var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                        message = $"{(_userManager.Options.Lockout.MaxFailedAccessAttempts - failedCount)} kez daha girerseniz hesabınız geçici olarak kilitlenecektir!";
                    }
                    else message = "Kullanıcı Adı veya şifre hatalı!";
                }
            }
            else message = "Girilen veriler hatalıdır!";
            ModelState.AddModelError("", message);
            return View(model);
        }
        [Authorize]//[Authorize(Roles ="Admin,Member")]
        public IActionResult GetUserInfo()
        {
            var userName = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminManager()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult MemberManager()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult MemberPage()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}