using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRegionRepository Regions { get; }

        IContactRepository Contacts { get; }

        IContactInfoRepository ContactInfo { get; }

        void Save();

        Task<int> SaveAsync();
    }
}
