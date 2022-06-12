using EduHomeBackendim.DAL;
using EduHomeBackendim.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendim.Areas.AdminE.Controllers
{
    [Area("AdminE")]
    public class NoticeController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public NoticeController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Notice> no = _context.Notices.ToList();
            return View(no);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Notice Notice = await _context.Notices.FindAsync(id);
            if (Notice == null)
            {
                return NotFound();
            }
            _context.Notices.Remove(Notice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice Notices)
        {

            Notice db = new Notice();
            db.Title = Notices.Title;
            db. Date= Notices.Date;
            await _context.Notices.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Notice Notices = await _context.Notices.FindAsync(id);
            if (Notices == null) return NotFound();
            return View(Notices);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Notice Notices, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notice existtitle = _context.Notices.FirstOrDefault(c => c.Title.ToLower() == Notices.Title.ToLower());
            Notice db = await _context.Notices.FindAsync(id);
            if (existtitle != null)
            {
                if (db != existtitle)
                {
                    ModelState.AddModelError("Title", "Title Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            }
            db.Title = Notices.Title;
            db.Date = Notices.Date;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Notice no = await _context.Notices.FindAsync(id);
            if (no == null)
            {
                return NotFound();
            }
            return View(no);
        }
    }
}
