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
    public class EfCoreRegionRepository : EfCoreGenericRepository<Region>, IRegionRepository
    {
        public EfCoreRegionRepository(ContactContext context) : base(context)
        {

        }

        public ContactContext ContactContext
        {
            get { return context as ContactContext; }
        }
    }
}
