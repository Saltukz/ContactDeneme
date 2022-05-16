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
    public class ContactInfoManager : IContactInfoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactInfoManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(ContactInfo entity)
        {
            _unitOfWork.ContactInfo.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(ContactInfo entity)
        {
            _unitOfWork.ContactInfo.Delete(entity);
            _unitOfWork.Save();
        }

        public async Task<List<ContactInfo>> GetAll()
        {
           return await _unitOfWork.ContactInfo.GetAll();
        }

        public async Task<ContactInfo> GetById(int id)
        {
            return await _unitOfWork.ContactInfo.GetById(id);
        }

        public void Update(ContactInfo entity)
        {
            _unitOfWork.ContactInfo.Update(entity);
            _unitOfWork.Save();
        }
    }
}
