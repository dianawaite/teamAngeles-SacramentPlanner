using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }
        [Required]
        public string? Conducting { get; set; }
        [Required]
        public string? OpeningPrayer { get; set; }
        [Required]
        public string? ClosingPrayer { get; set; }
        [Required]
        public string? OpeningHymn { get; set; }
        [Required]
        public string? SacramentHymn { get; set; }
        public string? IntermediateHymn { get; set; }
        [Required]
        public string? ClosingHymn { get; set; }
    }
}
