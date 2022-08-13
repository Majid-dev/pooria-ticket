using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PooriaTicket2.Models;
using PooriaTicket2.ViewModels;
using PooriTicket.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PooriaTicket2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
              
        private readonly PooriTicketDbContext _db;

        public HomeController(PooriTicketDbContext db)
        {
            _db = db;
        }
        [Authorize(Roles = "ادمین")]
        public IActionResult Index()
        {
            ClaimIndexViewModel Claims = new ClaimIndexViewModel();
            IEnumerable<User> UserList = _db.Users;
            Claims.Users = UserList;
            Claims.Claims = User?.Claims;
            return View(Claims);
        }
        

        //GET
        [AllowAnonymous]
        public IActionResult Create()
        {

            var roles = _db.Roles;
            var rolesList = new List<SelectListItem>();
            foreach(var role in roles)
            {
                rolesList.Add(new SelectListItem { Value = role.Id.ToString(), Text = role.Title});
            }
          
            ViewBag.UserRoles = rolesList;
            return View();
        }
        //POST
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Users.Any(x => x.UserName == user.UserName))
                {
                    SHA256 hash = SHA256.Create();
                    var passwordBytes = Encoding.Default.GetBytes(user.Password);
                    var hashBytes = hash.ComputeHash(passwordBytes);
                    user.Password = Convert.ToHexString(hashBytes);
                    
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    if (User.Identity.IsAuthenticated)
                        return RedirectToAction("Index");
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(user);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            
            if (id == null || id == 0)
                return NotFound();
            var user = _db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();

            var roles = _db.Roles;
            var rolesList = new List<SelectListItem>();
            foreach (var role in roles)
            {
                rolesList.Add(new SelectListItem { Value = role.Id.ToString(), Text = role.Title });
            }
            ViewBag.UserRoles = rolesList;

            return View(user);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        //GET
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
                return NotFound();
            var user = _db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();

            var roles = _db.Roles;
            var rolesList = new List<SelectListItem>();
            foreach (var role in roles)
            {
                rolesList.Add(new SelectListItem { Value = role.Id.ToString(), Text = role.Title });
            }
            ViewBag.UserRoles = rolesList;

            return View(user);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(User user)
        {
            
            var tickets = _db.Tickets.Where(x => x.UserId == user.Id).ToList();
            foreach (var ticket in tickets)
            {
                var responses1 = _db.Responses.Where(x => x.TicketId == ticket.Id).ToList();
                foreach (var response in responses1)
                {
                    _db.Responses.Remove(response);
                }
                _db.Tickets.Remove(ticket);
            }

            var responses = _db.Responses.Where(x => x.UserId == user.Id).ToList();
            foreach(var res in responses)
            {
                _db.Responses.Remove(res);
            }
                

            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
           
        }

    }
}