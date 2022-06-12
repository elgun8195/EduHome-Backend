using EduHomeBackendim.DAL;
using EduHomeBackendim.Extensions;
using EduHomeBackendim.Helpers;
using EduHomeBackendim.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EduHomeBackendim.Areas.AdminE.Controllers
{
    
    [Area("AdminE")]

    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public EventController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Event> events = _context.Events.ToList();
            return View(events);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Event even)
        {
            
           
            Event db = new Event();
            db.Title = even.Title;
            db.StartTime = even.StartTime;
            db.EndTime = even.EndTime;
            db.Location=even.Location;
            await _context.Events.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event   e = await _context.Events.FindAsync(id);
            if (e == null) return NotFound();
            return View(e);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Event even)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return NotFound();
            }
            Event db = await _context.Events.FindAsync(id);
            if (db == null) return NotFound();

            db.Title = even.Title;
            db.StartTime = even.StartTime;
            db.EndTime = even.EndTime;
            db.Location = even.Location;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event e = await _context.Events.FindAsync(id);
            if (e == null)
            {
                return NotFound();
            }
            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event e = await _context.Events.FindAsync(id);
            if (e == null)
            {
                return NotFound();
            }
            return View(e);
        }
    }
}
