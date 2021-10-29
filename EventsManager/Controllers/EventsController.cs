using EventsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Controllers
{
    public class EventsController : Controller
    {
        private IEventsDBContext _context;
        public EventsController(IEventsDBContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            var ExistingEvents = _context.Events.Include(x => x.Users).Where(x => !x.Deleted).OrderBy(x=>x.Date).ToList();
            return View(ExistingEvents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Event NewEvent)
        {
            if(ModelState.IsValid)
            {
                NewEvent.Active = true;
                _context.Events.Add(NewEvent);
                _context.SaveChanges();
                return RedirectToAction("List");
            };
            return View(NewEvent);
        }
        public IActionResult Edit(int Id)
        {
            return View(_context.Events.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        public IActionResult Edit(Event EditedEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Update(EditedEvent);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(EditedEvent);
        }
        [HttpPut]
        public IActionResult ChangeStatus(int Id)
        {
            var EditedEvent = _context.Events.FirstOrDefault(x => x.Id == Id);
            EditedEvent.Active = !EditedEvent.Active;
            _context.Events.Update(EditedEvent);
            _context.SaveChanges();
            return Json("Ok");
        }
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var DeletedEvent = _context.Events.FirstOrDefault(x => x.Id == Id);
            DeletedEvent.Deleted = true;
            _context.Events.Update(DeletedEvent);
            _context.SaveChanges();
            return Json("Ok");
        }

        public IActionResult GetAllUsers(int Id)
        {
            var EventUsers = _context.Events.Include(x => x.Users).FirstOrDefault(x => x.Id == Id).Users;
            return View();
        }
    }
}
