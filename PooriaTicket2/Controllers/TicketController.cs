using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PooriaTicket2.Models;
using PooriaTicket2.ViewModels;
using PooriTicket.Data;

namespace PooriaTicket2.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {

        private readonly PooriTicketDbContext _db;

        public TicketController(PooriTicketDbContext db)
        {
            _db = db;
        }       


        // GET: TicketController/Index/5
        public ActionResult Index(int id)
        {
            if (id == 0)
                return NotFound();
            var ticketList = _db.Tickets.Where(x => x.UserId == id).ToList();
            if (ticketList == null)
                return NotFound();

            //ViewBag.UserName = _db.Users.Where( u=>u.Id == id).Single().UserName;
            ViewBag.Id = id;

            TicketList tickets = new TicketList();
            tickets.Tickets = ticketList;
            tickets.UserName = _db.Users.Where( u => u.Id == id).Single().UserName;
            return View(tickets);
        }

        // GET: TicketController/Create
        public ActionResult Create(int UserId)
        {
            ViewBag.UserName = _db.Users.Where(u => u.Id == UserId).First().UserName;
            //ViewBag.Id = UserId;
            return View();
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _db.Tickets.Add(ticket);
                _db.SaveChanges();
                return RedirectToAction("Index", new {id = ticket.UserId});
            }
            return View(ticket);
        }

        // GET: TicketController/Details/1 => ticketId
        public ActionResult Details(int id)
        {
            var responseList = new ResponseListViewModel();
            var ticket = _db.Tickets.Where(t => t.Id == id).Single();
            responseList.Title = ticket.Title;
            responseList.TicketId = ticket.Id;
            var responses = _db.Responses.Where(t => t.TicketId == id).ToList();
            responseList.Responses = responses;

            return View(responseList);
        } 

        // POST: TicketController/Details/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ResponseViewModel model)
        {

            if (ModelState.IsValid)
            {

                _db.Responses.Add(new Response { TicketResponse = model.TicketResponse, TicketId = model.TicketId, UserId = model.UserId, Status="OK" });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = model.TicketId });
        }

        // GET: TicketController/Edit/5
        public ActionResult Edit(int id)
        {
            
            //ViewBag.Id = UserId;
            var ticket = _db.Tickets.Where(t => t.Id == id).Single();
            ViewBag.UserName = _db.Users.Where(u => u.Id == ticket.UserId).First().UserName;
            return View(ticket);
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _db.Tickets.Update(ticket);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = ticket.UserId });
            }
            return View(ticket);
        }



        //POST: inline response deletion
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete2(ResponseViewModel model)
        {
            var response = _db.Responses.FirstOrDefault(x=>x.Id==model.Id);
            _db.Responses.Remove(response);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = response.TicketId });
        }


        //GET
        public IActionResult Delete(int id)
        {

            if (id == null || id == 0)
                return NotFound();
            var ticket = _db.Tickets.FirstOrDefault(x => x.Id == id);
            if (ticket == null)
                return NotFound();


            return View(ticket);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var responses = _db.Responses.Where(x => x.TicketId == ticket.Id).ToList();
                foreach (var r in responses)
                {
                    _db.Responses.Remove(r);

                }
               

                _db.Tickets.Remove(ticket);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = ticket.UserId });
            }
            return View(ticket);
        }
    }
}
