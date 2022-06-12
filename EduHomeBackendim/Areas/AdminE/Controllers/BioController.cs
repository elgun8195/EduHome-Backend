using EduHomeBackendim.DAL;
using EduHomeBackendim.Extensions;
using EduHomeBackendim.Helpers;
using EduHomeBackendim.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendim.Areas.AdminE.Controllers
{
    [Area("AdminE")]
    public class BioController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public BioController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }

        public IActionResult Index()
        {
            Bio bio = _context.Bio.FirstOrDefault();
            return View(bio);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(Bio bio)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!bio.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            if (bio.Photo.CheckSize(9000))
            {
                ModelState.AddModelError("Photo", "Only accept image");
            }
            string filename = await bio.Photo.SaveImage(_webhost, "img/logo");
            Bio db = new Bio();
            db.Logo = filename;
            db.Number = bio.Number;
            db.Facebook = bio.Facebook;
            db.Twitter = bio.Twitter;
            db.Vcontact = bio.Vcontact;
            db.Pinterest = bio.Pinterest;
            await _context.Bio.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Bio bio =await _context.Bio.FindAsync(id);
            if (bio==null) return NotFound();   
            return View(bio);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Bio bio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return NotFound();
            }
            Bio dbBio = await _context.Bio.FindAsync(id);
            if (dbBio == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!bio.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (bio.Photo.CheckSize(9000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 9mg ola biler");
                return View();
            }
            string filename = await bio.Photo.SaveImage(_webhost,"img/logo");
            dbBio.Logo=filename;
            dbBio.Number = bio.Number;
            dbBio.Facebook = bio.Facebook;
            dbBio.Vcontact = bio.Vcontact;
            dbBio.Twitter = bio.Twitter;
            dbBio.Pinterest = bio.Pinterest;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bio bio = await _context.Bio.FindAsync(id);
            if (bio == null)
            {
                return NotFound();
            }
            _context.Bio.Remove(bio);
            Helper.DeleteImage( _webhost,"img/logo", bio.Logo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bio bio = await _context.Bio.FindAsync(id);
            if (bio == null)
            {
                return NotFound();
            }
            return View(bio);
        }
    }
}
