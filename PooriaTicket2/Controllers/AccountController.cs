using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PooriaTicket2.Models;
using PooriaTicket2.ViewModels;
using PooriTicket.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PooriaTicket2.Controllers
{
    public class AccountController : Controller
    {
        private readonly PooriTicketDbContext _db;
        public AccountController(PooriTicketDbContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        public string HashedPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashBytes = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashBytes);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
        {

            var user = _db.Users.Where(x => x.UserName == username)
                .Where(x => x.Password == HashedPassword(password))
                .FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, user.Role.Title)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                    , new ClaimsPrincipal(claimsIdentity));
                if (user.Role.Title == "ادمین")
                {
                    return Redirect(ReturnUrl == null ? "/" : ReturnUrl);
                }
                else
                {
                    return Redirect($"/Ticket/index/{user.Id.ToString()}");
                }
                
            }
            else
                return View();
        }

        //[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //GET

        public IActionResult Register()
        {
            //return View();
            return RedirectToAction("Create", "Home");
        }
        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Users.Any(x => x.UserName == user.UserName))
                {
                    //user.RoleId = 1;
                    //_db.Users.Add(user);
                    //_db.SaveChanges();
                    return RedirectToAction("Create", "Home");
                }
            }
            return View(user);
        }
    }
}
