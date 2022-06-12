using EduHomeBackendim.DAL;
using EduHomeBackendim.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendim.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private AppDbContext _context;

        public FooterViewComponent(AppDbContext appDbContext)
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
