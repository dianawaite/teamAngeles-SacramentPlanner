using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Member")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string? Name { get; set; }

        public bool Bishopric { get; set; }

    }
}
