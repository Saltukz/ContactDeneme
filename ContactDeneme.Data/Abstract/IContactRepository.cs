using ContactDeneme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Data.Abstract
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<List<Contact>> getMyContacts(string userid);

        

       Task<List<Contact>> getMyContactsWithInfo(string userid);


        Task<List<int>> getReports();

        Task<int> getCountMax(int region);
        Task<List<Contact>> getTelephoneCount(int region);
    }
}
