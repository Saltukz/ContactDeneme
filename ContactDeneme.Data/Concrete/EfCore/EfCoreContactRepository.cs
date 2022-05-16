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

        public Task<List<Contact>> getMyContacts(string userid)
        {
            return ContactContext.Contacts.Where(x => x.UserId == userid).ToListAsync();


        }

        public Task<List<Contact>> getMyContactsWithInfo(string userid)
        {
            return ContactContext.Contacts.Where(x=> x.UserId == userid).Include(i=>i.ContactInfo).ToListAsync();
        }
    }
}
