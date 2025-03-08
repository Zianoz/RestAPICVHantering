using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RestAPICVHantering.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; } // PK

        [StringLength(30, MinimumLength = 1), Required]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 1), Required]
        public string LastName { get; set; }

        [StringLength(30, MinimumLength = 3), Required]
        public string Email { get; set; }

        [Range(1, 12), Required]
        public int Phone { get; set; }

        // Navigation Properties
        public virtual Education Education { get; set; }
        public virtual WorkExperience WorkExperience { get; set; }

    }
} 
