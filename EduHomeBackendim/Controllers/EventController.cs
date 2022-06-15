using EduHomeBackendim.DAL;
using EduHomeBackendim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackendim.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<EventDetail> events=_context.EventDetails.ToList();
            return View(events);
        }

        public IActionResult Detail(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            EventDetail eventDetail = _context.EventDetails.FirstOrDefault(x => x.Id == id);
            if (eventDetail == null)
            {
                return NotFound();
            }
            return View(eventDetail);
        }
    }
}
