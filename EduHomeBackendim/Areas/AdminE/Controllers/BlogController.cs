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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.ToList();
            return View(blogs);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Blog blog =await _context.Blogs.FindAsync(id);
            if (blog==null)
            {
                return NotFound();
            }
            _context.Blogs.Remove(blog);
            Helper.DeleteImage(_webhost, "img/blog",blog.Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo","Ancaq sekil sece bilersiniz");
            }
            if (blog.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename =await blog.Photo.SaveImage(_webhost,"img/blog");
            Blog dbBlog = new Blog();
            dbBlog.Image = filename;
            dbBlog.Author = blog.Author;
            dbBlog.WriteTime=blog.WriteTime;
            dbBlog.Title = blog.Title;
            dbBlog.CommentCount=blog.CommentCount;
            await _context.Blogs.AddAsync(dbBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Blog blogs =await _context.Blogs.FindAsync(id);
            if (blogs==null) return NotFound();
            return View(blogs);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Blog blog,int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (blog.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Blog existAuthor= _context.Blogs.FirstOrDefault(c => c.Author.ToLower() == blog.Author.ToLower());
            Blog dbBlog =await _context.Blogs.FindAsync(id);
            if (existAuthor != null)
            {
                if (dbBlog != existAuthor)
                {
                    ModelState.AddModelError("Author", "Author Already Exist");
                    return View();
                }
            }
            if (dbBlog==null)
            {
                return NotFound();
            }

            string filename = await blog.Photo.SaveImage(_webhost, "img/blog");
            dbBlog.Image = filename;
            dbBlog.Author=blog.Author;
            dbBlog.WriteTime = blog.WriteTime;
            dbBlog.Title=blog.Title;
            dbBlog.CommentCount=blog.CommentCount;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Blog blog =await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
    }
}
