using EduHomeBackendim.DAL;
using EduHomeBackendim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendim.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private AppDbContext _context;

        public HeaderViewComponent(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio bio = _context.Bio.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}
