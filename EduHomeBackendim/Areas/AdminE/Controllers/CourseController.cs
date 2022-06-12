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
    
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public CourseController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<CourseOffer> course = _context.CourseOffers.ToList();
            return View(course);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CourseOffer course)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!course.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            if (course.Photo.CheckSize(9000))
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            string filename = await course.Photo.SaveImage(_webhost, "img/course");
            CourseOffer db = new CourseOffer();
            db.ImageUrl = filename;
            db.Subtitle = course.Subtitle;
            db.Title = course.Title;
            await _context.CourseOffers.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CourseOffer course = await _context.CourseOffers.FindAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CourseOffer course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return NotFound();
            }
            CourseOffer db = await _context.CourseOffers.FindAsync(id);
            if (db == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!db.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (db.Photo.CheckSize(9000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 9mg ola biler");
                return View();
            }
            string filename = await course.Photo.SaveImage(_webhost, "img/course");
            db.ImageUrl = filename;
            db.Subtitle = course.Subtitle;
            db.Title = course.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CourseOffer course = await _context.CourseOffers.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.CourseOffers.Remove(course);
            Helper.DeleteImage(_webhost, "img/course", course.ImageUrl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CourseOffer course = await _context.CourseOffers.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
