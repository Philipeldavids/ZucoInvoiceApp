using DataLayer.Database;
using DataLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class CompanyProfileRepository : ICompanyProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyProfile?> GetByUserIdAsync(Guid userId)
        {
            return await _context.CompanyProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task CreateOrUpdateAsync(CompanyProfile profile)
        {
            var existing = await GetByUserIdAsync(profile.UserId);

            if (existing == null)
            {
                _context.CompanyProfiles.Add(profile);
            }
            else
            {
                existing.CompanyName = profile.CompanyName;
                existing.Address = profile.Address;
                existing.PhoneNumber = profile.PhoneNumber;
                existing.Email = profile.Email;
                existing.Website = profile.Website;
                existing.LogoUrl = profile.LogoUrl;
            }

            await _context.SaveChangesAsync();
        }
    }
}
