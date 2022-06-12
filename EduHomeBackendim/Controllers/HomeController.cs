using EduHomeBackendim.DAL;
using EduHomeBackendim.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EduHomeBackendim.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Slider = _context.Sliders.ToList();
            homeVM.Notice = _context.Notices.ToList();
            homeVM.NoticeAds=_context.NoticeAdss.ToList();
            homeVM.WhyChoose = _context.whyChoose.FirstOrDefault();
            homeVM.CourseOffers=_context.CourseOffers.ToList();
            homeVM.Testimonial = _context.Testimonial.FirstOrDefault();
            homeVM.Events=_context.Events.ToList();
            homeVM.Blogs=_context.Blogs.ToList();
            homeVM.Bio = _context.Bio.FirstOrDefault();
            return View(homeVM);
        }
    }
}
