using EduHomeBackendim.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHomeBackendim.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<NoticeAds> NoticeAdss { get; set; }
        public DbSet<WhyChoose> whyChoose { get; set; }
        public DbSet<CourseOffer> CourseOffers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Bio> Bio { get; set; }
    }
}
