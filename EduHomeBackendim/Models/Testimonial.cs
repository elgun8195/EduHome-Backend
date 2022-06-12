using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackendim.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Image { get; set; }
    
        public string Name { get; set; }
     
        public string Title { get; set; }
       
        public string Position { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
