using ContactDeneme.Data.Abstract;
using ContactDeneme.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Data.Concrete.EfCore
{
    public class EfCoreContactRepository : EfCoreGenericRepository<Contact>, IContactRepository
    {
        public EfCoreContactRepository(ContactContext context) : base(context)
        {

        }

        public ContactContext ContactContext
        {
            get { return context as ContactContext; }
        }

        public Task<int> getCountMax(int region)
        {
             var sonuc = ContactContext.Contacts.Count(c => c.RegionId == region);

            return Task.FromResult(sonuc);
        }

        public Task<List<Contact>> getMyContacts(string userid)
        {
            return ContactContext.Contacts.Where(x => x.UserId == userid).ToListAsync();


        }

        public Task<List<Contact>> getMyContactsWithInfo(string userid)
        {
            return ContactContext.Contacts.Where(x=> x.UserId == userid).Include(i=>i.ContactInfo).ToListAsync();
        }

        public Task<List<int>> getReports()
        {
            var sonuc = ContactContext.Contacts
                 .GroupBy(d => d.RegionId)
                 .Select(x => x.Key).OrderBy(x => x);




            return sonuc.ToListAsync();

        }

        public Task<List<Contact>> getTelephoneCount(int region)
        {

            var contacts = ContactContext.Contacts.Where(x => x.RegionId == region).Include(i => i.ContactInfo).Where(a => a.ContactInfo.Telephone != null).ToListAsync();



            return contacts;
        }
    }
}
