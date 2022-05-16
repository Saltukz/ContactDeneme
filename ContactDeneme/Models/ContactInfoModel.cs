using System.ComponentModel.DataAnnotations;

namespace ContactDeneme.Models
{
    public class ContactInfoModel
    {

        public string? Telephone { get; set; }
    
        public string? Email { get; set; }

        [Required]
        public int ContactId { get; set; }
       
    }


    public class ContactInfoResponse
    {
        public int infoid { get; set; }
        public string? Telephone { get; set; }

        public string? Email { get; set; }

        public int ContactId { get; set; }
    }


   
}
