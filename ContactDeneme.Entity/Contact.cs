using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Entity
{
    public partial class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string Surname { get; set; } = null!;
        [StringLength(100)]
        public string Company   { get; set; } = null!;

        public string UserId { get; set; } = null!;
    
        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        [InverseProperty("Contacts")]
        public virtual Region Region { get; set; } = null!;
        [InverseProperty("Contact")]
        public virtual ContactInfo ContactInfo { get; set; } = null!;

    }
}
