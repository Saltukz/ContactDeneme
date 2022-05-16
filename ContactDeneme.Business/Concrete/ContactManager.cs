using ContactDeneme.Business.Abstract;
using ContactDeneme.Data.Abstract;
using ContactDeneme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Contact entity)
        {
            _unitOfWork.Contacts.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Contact entity)
        {
            _unitOfWork.Contacts.Delete(entity);
            _unitOfWork.Save();
        }

        public async Task<List<Contact>> GetAll()
        {
           return await _unitOfWork.Contacts.GetAll();
        }

        public async Task<Contact> GetById(int id)
        {
            return await _unitOfWork.Contacts.GetById(id);
        }

        public async Task<List<Contact>> GetMyContacts(string userid)
        {
            return await _unitOfWork.Contacts.getMyContacts(userid);
        }

        public async Task<List<Contact>> GetMyContactsWithInfo(string userid)
        {
            return await _unitOfWork.Contacts.getMyContactsWithInfo(userid);
        }

        public void Update(Contact entity)
        {
            _unitOfWork.Contacts.Update(entity);
            _unitOfWork.Save();
        }
    }
}
