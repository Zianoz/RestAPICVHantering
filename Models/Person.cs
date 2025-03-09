using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RestAPICVHantering.Models
{
    public class Person
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonID { get; set; } // PK

        [StringLength(30, MinimumLength = 1), Required]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 1), Required]
        public string LastName { get; set; }

        [StringLength(30, MinimumLength = 3), Required]
        public string Email { get; set; }

        [StringLength(12, MinimumLength = 1), Required]
        public string Phone { get; set; }

        // Navigation Properties
        public virtual List<Education> Education { get; set; } = new();
        public virtual List<WorkExperience> WorkExperience { get; set; } = new();

    }
} 
