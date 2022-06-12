using EduHomeBackendim.DAL;
using EduHomeBackendim.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
namespace EduHomeBackendim.Areas.AdminE.Controllers
{
   
    [Area("AdminE")]
    public class WhyChooseController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public WhyChooseController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            WhyChoose WhyChoose = _context.whyChoose.FirstOrDefault();
            return View(WhyChoose);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WhyChoose WhyChoose = await _context.whyChoose.FindAsync(id);
            if (WhyChoose == null)
            {
                return NotFound();
            }
            _context.whyChoose.Remove(WhyChoose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WhyChoose WhyChoose)
        {

            WhyChoose db = new WhyChoose();
            db.Title = WhyChoose.Title;
            db.Subtitle = WhyChoose.Title;
            db.Descript = WhyChoose.Descript;

            await _context.whyChoose.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WhyChoose WhyChoose = await _context.whyChoose.FindAsync(id);
            if (WhyChoose == null) return NotFound();
            return View(WhyChoose);
        }
        [HttpPost]
        public async Task<IActionResult> Update(WhyChoose WhyChoose, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WhyChoose existname = _context.whyChoose.FirstOrDefault(c => c.Title.ToLower() == WhyChoose.Title.ToLower());
            WhyChoose db = await _context.whyChoose.FindAsync(id);
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

            db.Title = WhyChoose.Title;
            db.Descript = WhyChoose.Descript;
            db.Subtitle = WhyChoose.Subtitle;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WhyChoose WhyChoose = await _context.whyChoose.FindAsync(id);
            if (WhyChoose == null)
            {
                return NotFound();
            }
            return View(WhyChoose);
        }
    }
}
