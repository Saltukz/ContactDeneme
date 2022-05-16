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
    public class RegionManager : IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegionManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Region entity)
        {
            _unitOfWork.Regions.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Region entity)
        {
            _unitOfWork.Regions.Delete(entity);
            _unitOfWork.Save();
        }

        public async Task<List<Region>> GetAll()
        {
           return await _unitOfWork.Regions.GetAll();
        }

        public async Task<Region> GetById(int id)
        {
            return await _unitOfWork.Regions.GetById(id);
        }

        public void Update(Region entity)
        {
            _unitOfWork.Regions.Update(entity);
            _unitOfWork.Save();
        }
    }
}
