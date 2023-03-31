using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class MeetingCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string? Congregation { get; set; }
        [DataType(DataType.Date)]

        [Display(Name = "Meeting Date")]
        public DateTime MeetingDate { get; set; }
        
        //[Required]
        //[StringLength(60, MinimumLength = 3)]
        //public string? Conducting { get; set; }
        
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Opening Prayer")]
        public string? OpeningPrayer { get; set; }
        [Required]

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Closing Prayer")]
        public string? ClosingPrayer { get; set; }
        [Required]

        [Display(Name = "Opening Hymn")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string? OpeningHymn { get; set; }
        [Required]

        [Display(Name = "Sacrament Hymn")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string? SacramentHymn { get; set; }
        [Display(Name = "Intermediate Hymn (Optional)")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string? IntermediateHymn { get; set; }
        [Required]

        [Display(Name = "Closing Hymn")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string? ClosingHymn { get; set; }

        public List<SelectListItem> Conducting { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Adventure", Text = "Adventure" },
            new SelectListItem { Value = "Christmas", Text = "Christmas" },
            new SelectListItem { Value = "Comedy", Text = "Comedy" },
            new SelectListItem { Value = "Drama", Text = "Drama" },
            new SelectListItem { Value = "Romance", Text = "Romance"  },
            new SelectListItem { Value = "Thriller", Text = "Thriller" }
        };

    }
}
