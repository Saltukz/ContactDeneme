using ContactDeneme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Business.Abstract
{
    public interface IContactService
    {
        Task<Contact> GetById(int id);

        Task<List<Contact>> GetAll();

        void Create(Contact entity);

        void Update(Contact entity);

        void Delete(Contact entity);

        Task<List<Contact>> GetMyContacts(string userid);

        Task<List<Contact>> GetMyContactsWithInfo(string userid);

        Task<List<int>> getReports();

        Task<int> getCountMax(int region);

        Task<List<Contact>> getTelephoneCount(int region);


    }
}
