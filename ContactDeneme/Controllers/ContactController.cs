using ContactDeneme.Business.Abstract;
using ContactDeneme.Entity;
using ContactDeneme.Identity;
using ContactDeneme.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace ContactDeneme.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        private readonly IContactInfoService _contactInfoService;
      

        public ContactController(IContactService contactService, IConfiguration configuration, IDistributedCache cache,IContactInfoService contactInfoService)
        {
            _contactService = contactService;
            _configuration = configuration;
            _cache = cache;
            _contactInfoService = contactInfoService;
        
        }

      
        [HttpPost("createNewContact")]
        
        public IActionResult CreateContact([FromBody] ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.Claims.FirstOrDefault().Value.ToString();

              

                Contact contact = new Contact
                {
                    Name = model.Name.Trim(),
                    Surname = model.Surname.Trim(),
                    Company  = model.Company.Trim(),
                    UserId = userId,
                    RegionId = model.RegionId,
                };
                _contactService.Create(contact);



                return Ok(contact.ContactId);
            }
            return BadRequest(ModelState);


        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Contact contact = await _contactService.GetById(id);

            if (contact == null)
            {
                return NotFound("Kayıt Bulunamadı.");
            }

            if(contact.UserId == HttpContext.User.Claims.FirstOrDefault().Value.ToString())
            {
                _contactService.Delete(contact);
            }
            

            return Ok();

        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditContact([FromRoute] int id,[FromBody] ContactModel model)
        {

            if (ModelState.IsValid)
            {
                Contact contact = await _contactService.GetById(id);

                if (contact == null)
                {
                    return NotFound("Kayıt bulunamadı.");
                }

                if (contact.UserId == HttpContext.User.Claims.FirstOrDefault().Value.ToString())
                {

                    contact.Name = model.Name.Trim();
                    contact.Surname = model.Surname.Trim();
                    contact.Company = model.Company.Trim();
                    contact.RegionId = model.RegionId;

                    _contactService.Update(contact);
                    return Ok(contact.ContactId);
                }

                return Unauthorized();

            }
            return BadRequest(ModelState);
        }

        [HttpGet("mycontacts")]
        public async Task<IActionResult> getContacts()
        {

            
            var userid = HttpContext.User.Claims.FirstOrDefault().Value.ToString();

            string cacheKey = userid;

            byte[] cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                var cachedContacts = JsonSerializer.Deserialize<List<Contact>>(cachedDataString);

                List<ContactModel> cachedresponseLists = new List<ContactModel>();

                foreach (Contact contact in cachedContacts)
                {
                    cachedresponseLists.Add(new ContactModel
                    {
                        Company = contact.Company,
                        Name = contact.Name,
                        Surname = contact.Surname,
                        RegionId = contact.RegionId,
                    });
                }

                return Ok(cachedresponseLists);
            }
            else
            {
                List<Contact> contacts = await _contactService.GetMyContacts(userid);

                List<ContactModel> responseLists = new List<ContactModel>();
                foreach (Contact contact in contacts)
                {
                    responseLists.Add(new ContactModel
                    {
                        Company = contact.Company,
                        Name = contact.Name,
                        Surname = contact.Surname,
                        RegionId = contact.RegionId,
                    });
                }
                return Ok(responseLists);
            }
            
           




        }



        [HttpPost("createinfo/{contactid}")]

        public async Task<IActionResult> CreateContactInfo([FromRoute] int contactid,[FromBody] ContactInfoModel model)
        {
            if (ModelState.IsValid)
            {
                


                Contact contact = await _contactService.GetById(contactid);

                if (contact == null)
                {
                    return NotFound("Kayıt bulunamadı.");
                }

                if (contact.UserId == HttpContext.User.Claims.FirstOrDefault().Value.ToString())
                {
                    ContactInfo contactInfo = new ContactInfo();
                    contactInfo.Telephone =model.Telephone;
                    contactInfo.Email =model.Email;
                    contactInfo.ContactId = contact.ContactId;

                    _contactInfoService.Create(contactInfo);


                    ContactInfoResponse responseModel = new ContactInfoResponse{
                        infoid = contactInfo.InfoId,
                        Telephone=contactInfo.Telephone,
                        Email=contactInfo.Email,
                        ContactId=contactInfo.ContactId,
                    };

                    return Ok(responseModel);
                }


                return Unauthorized();
            }
            return BadRequest(ModelState);


        }



        [HttpDelete("deleteinfo/{id}")]
        public async Task<IActionResult> DeleteInfo([FromRoute] int id)
        {
            ContactInfo contactinfo = await _contactInfoService.GetById(id);

            if (contactinfo == null)
            {
                return NotFound("Kayıt Bulunamadı.");
            }

            _contactInfoService.Delete(contactinfo);


            return Ok();

        }




        [HttpGet("myAllContacts")]
        public async Task<IActionResult> getAllContactWithInfo()
        {


            var userid = HttpContext.User.Claims.FirstOrDefault().Value.ToString();

            string cacheKey = userid;

            byte[] cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                var cachedContacts = JsonSerializer.Deserialize<List<Contact>>(cachedDataString);

                List<ContactModelWithInfo> cachedresponseLists = new List<ContactModelWithInfo>();

                foreach (Contact contact in cachedContacts)
                {
                    cachedresponseLists.Add(new ContactModelWithInfo
                    {
                        Company = contact.Company,
                        Name = contact.Name,
                        Surname = contact.Surname,
                        RegionId = contact.RegionId,
                        contactinfoModel = new ContactInfoModel
                        {
                            Telephone = contact.ContactInfo.Telephone,
                            Email = contact.ContactInfo.Email,
                            ContactId = contact.ContactId
                        }

                    });
                }

                return Ok(cachedresponseLists);
            }
            else
            {
                List<Contact> contacts = await _contactService.GetMyContactsWithInfo(userid);

                List<ContactModelWithInfo> responseLists = new List<ContactModelWithInfo>();
                foreach (Contact contact in contacts)
                {
                    responseLists.Add(new ContactModelWithInfo
                    {
                        Company = contact.Company,
                        Name = contact.Name,
                        Surname = contact.Surname,
                        RegionId = contact.RegionId,
                        contactinfoModel= new ContactInfoModel
                        {
                            Telephone = contact.ContactInfo.Telephone,
                            Email = contact.ContactInfo.Email,
                            ContactId = contact.ContactId
                        }
                    });
                }
                return Ok(responseLists);
            }






        }


        [HttpGet("getreports")]
        public async Task<IActionResult> getReports()
        {
            var sonuc = await _contactService.getReports();

            var countContacts = await _contactService.getCountMax(sonuc[0]);

            var telephoneCount = await _contactService.getTelephoneCount(sonuc[0]);


            var model = new ReportModel
            {
                RegionList = sonuc,
                Count = countContacts,
                telephoneCount = telephoneCount.Count
            };

            return Ok(model);
        }
    }

}
