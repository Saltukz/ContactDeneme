using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactDeneme.Entity
{
    [Index(nameof(RegionName), IsUnique = true)]
    public partial class Region
    {
        public Region()
        {
            Contacts = new HashSet<Contact>();
        }
        [Key]
        public int RegionId { get; set; }
        [StringLength(100)]
        public string RegionName { get; set; } = null!;
        [InverseProperty("Region")]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
