using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPICVHantering.Models
{
    public class WorkExperience
    {
        [Key]
        public int ExperienceID { get; set; } // PK

        [StringLength(30, MinimumLength = 1), Required]
        public string CompanyName { get; set; }

        [StringLength(30, MinimumLength = 1), Required]
        public string JobTitle { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string JobDescription { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey("Person")]
        public int PersonID { get; set; } // FK
    }
}
