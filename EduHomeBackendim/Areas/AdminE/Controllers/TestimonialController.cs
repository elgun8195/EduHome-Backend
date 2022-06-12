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
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public TestimonialController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            Testimonial testimonial = _context.Testimonial.FirstOrDefault();
            return View(testimonial);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Testimonial Testimonial = await _context.Testimonial.FindAsync(id);
            if (Testimonial == null)
            {
                return NotFound();
            }
            _context.Testimonial.Remove(Testimonial);
            Helper.DeleteImage(_webhost, "img/slider", Testimonial.Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial Testimonial)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Testimonial.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Testimonial.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8mb ola biler");
            }

            string filename = await Testimonial.Photo.SaveImage(_webhost, "img/testimonial");
            Testimonial db = new Testimonial();
            db.Image = filename;
            db.Title = Testimonial.Title;
            db.Name = Testimonial.Name;
            db.Position=Testimonial.Position;

            await _context.Testimonial.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Testimonial Testimonial = await _context.Testimonial.FindAsync(id);
            if (Testimonial == null) return NotFound();
            return View(Testimonial);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Testimonial Testimonial, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Testimonial.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Testimonial.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Testimonial existname = _context.Testimonial.FirstOrDefault(c => c.Name.ToLower() == Testimonial.Name.ToLower());
            Testimonial db = await _context.Testimonial.FindAsync(id);
            if (existname != null)
            {
                if (db != existname)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            }

            string filename = await Testimonial.Photo.SaveImage(_webhost, "img/testimonial");
            db.Image = filename;
            db.Title = Testimonial.Title;
            db.Name = Testimonial.Name;
            db.Position = Testimonial.Position;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Testimonial Testimonial = await _context.Testimonial.FindAsync(id);
            if (Testimonial == null)
            {
                return NotFound();
            }
            return View(Testimonial);
        }
    }
}
