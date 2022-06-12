using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackendim.Areas.AdminE.Controllers
{
    [Area("AdminE")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
