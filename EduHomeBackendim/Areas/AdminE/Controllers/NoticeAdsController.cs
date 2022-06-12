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
    public class NoticeAdsController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public NoticeAdsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<NoticeAds> no = _context.NoticeAdss.ToList();
            return View(no);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            NoticeAds NoticeAds = await _context.NoticeAdss.FindAsync(id);
            if (NoticeAds == null)
            {
                return NotFound();
            }
            _context.NoticeAdss.Remove(NoticeAds);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoticeAds NoticeAds)
        {

            NoticeAds db = new NoticeAds();
            db.Title = NoticeAds.Title;
            db.Subtitle = NoticeAds.Subtitle;
            await _context.NoticeAdss.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            NoticeAds NoticeAds = await _context.NoticeAdss.FindAsync(id);
            if (NoticeAds == null) return NotFound();
            return View(NoticeAds);
        }
        [HttpPost]
        public async Task<IActionResult> Update(NoticeAds NoticeAds, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoticeAds existtitle = _context.NoticeAdss.FirstOrDefault(c => c.Title.ToLower() == NoticeAds.Title.ToLower());
            NoticeAds db = await _context.NoticeAdss.FindAsync(id);
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
             db.Title = NoticeAds.Title;
            db.Subtitle = NoticeAds.Subtitle;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            NoticeAds no = await _context.NoticeAdss.FindAsync(id);
            if (no == null)
            {
                return NotFound();
            }
            return View(no);
        }
    }
}
