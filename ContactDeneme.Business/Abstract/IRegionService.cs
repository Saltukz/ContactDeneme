using ContactDeneme.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Business.Abstract
{
    public interface IRegionService
    {
        Task<Region> GetById(int id);

        Task<List<Region>> GetAll();

        void Create(Region entity);

        void Update(Region entity);

        void Delete(Region entity);
    }
}
