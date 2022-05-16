using ContactDeneme.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactDeneme.Models
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Company { get; set; } = null!;
       
        [Required]
        public int RegionId { get; set; }

        
        public ContactInfoModel contactinfoModel { get; set; }
    }
}
