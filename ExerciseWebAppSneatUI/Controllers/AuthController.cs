using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExerciseWebAppSneatUI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseWebAppSneatUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ExerciseWebAppSneatUiContext _context;

        public AuthController(ExerciseWebAppSneatUiContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        // POST: AccountUser login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountUser user)
        {
            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
                AccountUser accountUserData = await _context
                                      .AccountUsers
                                      .Where(u => u.UserName == user.UserName && u.Password == user.Password)
                                      .FirstOrDefaultAsync();

                ClaimsIdentity isIdentity = new();
                bool isAuthenticated = false;

                if (accountUserData != null)
                {
                    isIdentity = CreateClaimAccountUserData(accountUserData);
                    isAuthenticated = true;
                }

                if (isAuthenticated)
                {
                    var isIdentity_AccountUser = new ClaimsPrincipal(isIdentity);

                    // Store the user info to Session
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, isIdentity_AccountUser);

                    return RedirectToAction("Index", "Classes");
                }
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }

        private static ClaimsIdentity CreateClaimAccountUserData(AccountUser accountuser)
        {
            var accountUserID = accountuser.Id.ToString();

            var claims = new Claim[]
            {
                new Claim("ct:CustomClaim:UserID", accountUserID),
                new Claim("ct:CustomClaim:UserName", accountuser.UserName),
                new Claim("ct:CustomClaim:Email", accountuser.Email),
            };

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
