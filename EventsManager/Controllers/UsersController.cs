using EventsManager.Models;
using EventsManager.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Controllers
{
    public class UsersController : Controller
    {
        private IEventsDBContext _context;
        public UsersController(IEventsDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register(int Id)
        {
            var CheckEvent = _context.Events.FirstOrDefault(x => x.Id == Id);
            if (_context.Events.FirstOrDefault(x => x.Id == Id) == null)
            {
                TempData["alert"] = "This event does not exist!";
                return RedirectToAction("List", "Events");
            }
            else if (CheckEvent.Deleted || !CheckEvent.Active)
            {
                TempData["alert"] = "This event is either deleted or deactivated!";
                return RedirectToAction("List", "Events");
            }
            var NewUser = new UserRegisteration
            {
                EventId = Id
            };
            return View(NewUser);
        }
        [HttpPost]
        public IActionResult Register(UserRegisteration NewUser)
        {
            //Check if the user exists
            var Event = _context.Events.FirstOrDefault(x => x.Id == NewUser.EventId);
            var Existed = _context.Users.Include(x => x.Events).FirstOrDefault(x => x.Mobile == NewUser.Mobile);
            if(Existed != null)
            {
                //Check if the user is registered in the event
                var IsRegistered = Existed.Events.FirstOrDefault(x => x.Id == NewUser.EventId) != null;
                if (IsRegistered)
                {
                    ViewBag.Alert = "This user has already registered for the event!";
                    return View(NewUser);
                }
                else
                {
                    Existed.Events.Add(Event);
                    Event.UsersNo++;
                    _context.SaveChanges();
                    return RedirectToAction("List", "Events");
                }
            }
            else
            {
                var RegisterUser = new User
                {
                    Mobile = NewUser.Mobile,
                    Name = NewUser.Name
                };
                RegisterUser.Events.Add(Event);
                _context.Users.Add(RegisterUser);
                Event.UsersNo++;
                _context.SaveChanges();
                return RedirectToAction("List", "Events");
            }
        }
    }
}
