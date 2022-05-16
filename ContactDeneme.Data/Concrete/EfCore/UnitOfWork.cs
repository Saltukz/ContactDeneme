using ContactDeneme.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContactContext _context;

        public UnitOfWork(ContactContext context)
        {
            _context = context;
        }
        private EfCoreRegionRepository _regionRepository;
        private EfCoreContactRepository _contactRepository;
        private EfCoreContactInfoRepository _efCoreContactInfoRepository;
        public IRegionRepository Regions => _regionRepository = _regionRepository ?? new EfCoreRegionRepository(_context);

        public IContactRepository Contacts => _contactRepository = _contactRepository ?? new EfCoreContactRepository(_context);

        public IContactInfoRepository ContactInfo => _efCoreContactInfoRepository = _efCoreContactInfoRepository ?? new EfCoreContactInfoRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
