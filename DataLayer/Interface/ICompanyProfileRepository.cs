using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface ICompanyProfileRepository
    {
        Task<CompanyProfile?> GetByUserIdAsync(Guid userId);
        Task CreateOrUpdateAsync(CompanyProfile profile);
    }
}
