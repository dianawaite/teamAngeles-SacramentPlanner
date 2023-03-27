using SacramentPlanner.Models;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class Speaker
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Meeting { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Subject { get; set; }
    }
}