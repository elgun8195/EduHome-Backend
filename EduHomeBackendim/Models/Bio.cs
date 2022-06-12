using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackendim.Models
{
    public class Bio
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Logo { get; set; }
        public string Facebook { get; set; }
        public string Vcontact { get; set; }
        public string Twitter { get; set; }
        public string Pinterest { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
