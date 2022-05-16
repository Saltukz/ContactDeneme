using ContactDeneme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Business.Abstract
{
    public interface IContactInfoService
    {
        Task<ContactInfo> GetById(int id);

        Task<List<ContactInfo>> GetAll();

        void Create(ContactInfo entity);

        void Update(ContactInfo entity);

        void Delete(ContactInfo entity);

       
    }
}
