using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace RestAPICVHantering.Models
{
    public class Education
    {
        [Key]
        public int EducationID { get; set; } // PK

        [StringLength(30, MinimumLength = 1), Required]
        public string SchoolName { get; set; }

        [StringLength(30, MinimumLength = 1), Required]
        public string Degree { get; set; }

        [StringLength(30, MinimumLength = 1), Required]
        public string FieldOfStudy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Person")]
        public int PersonID { get; set; } // FK

        // Navigation Property
        public virtual Person Person { get; set; }
    }
}
