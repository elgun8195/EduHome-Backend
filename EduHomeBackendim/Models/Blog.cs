using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackendim.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime WriteTime { get; set; }
        public int CommentCount { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
