using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Entity
{
    [Index(nameof(ContactId),IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Telephone), IsUnique = true)]
    public partial class ContactInfo
    {
        [Key]
        public int InfoId { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? Telephone { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }

        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        [InverseProperty("ContactInfo")]
        public virtual Contact Contact { get; set; } = null!;


    }
}
