using EduHomeBackendim.Models;
using System.Collections.Generic;

namespace EduHomeBackendim.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Notice> Notice { get; set; }
        public IEnumerable<NoticeAds> NoticeAds { get; set; }
        public WhyChoose WhyChoose { get; set; }
        public IEnumerable<CourseOffer> CourseOffers { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public Testimonial Testimonial { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public Bio Bio { get; set; }
    }
}
