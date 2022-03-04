using EyIdentityApp.Context;
using EyIdentityApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EyIdentityApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly EyContext _eyContext;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, EyContext eyContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _eyContext = eyContext;
        }

        public async Task<IActionResult> Index()
        {
            var query = _userManager.Users;

            var UserList = _eyContext.Users.Join(_eyContext.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new
            {
                user,
                userRole
            }).Join(_eyContext.Roles,two=>two.userRole.RoleId,role=>role.Id,(two,role)=>new { two.user,two.userRole,role}).Where(x => x.role.Name != "Admin").Select(x => new AppUser
            {
                Id = x.user.Id,
                Email = x.user.Email,
                AccessFailedCount = x.user.AccessFailedCount,
                ConcurrencyStamp = x.user.ConcurrencyStamp,
                EmailConfirmed = x.user.EmailConfirmed,
                ImagePath = x.user.ImagePath,
                LockoutEnabled = x.user.LockoutEnabled,
                LockoutEnd = x.user.LockoutEnd,
                NormalizedEmail = x.user.NormalizedEmail,
                NormalizedUserName = x.user.NormalizedUserName,
                PasswordHash = x.user.PasswordHash,
                PhoneNumber = x.user.PhoneNumber,
                PhoneNumberConfirmed = x.user.PhoneNumberConfirmed,
                SecurityStamp = x.user.SecurityStamp,
                TwoFactorEnabled = x.user.TwoFactorEnabled,
                UserName = x.user.UserName
            }).ToList();

            var users = await _userManager.GetUsersInRoleAsync("Member");
            return View(UserList);
        }
    }
}
