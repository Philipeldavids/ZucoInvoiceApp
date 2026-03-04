using DataLayer.Interface;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CompanyProfileService
    {
        private readonly ICompanyProfileRepository _repo;
        private readonly ILogger<CompanyProfileService> _logger;
        private readonly IInvoiceService _service;

        public CompanyProfileService(IInvoiceService service, ICompanyProfileRepository repo, ILogger<CompanyProfileService> logger)
        {
            _repo = repo;
            _logger = logger;
            _service = service;
        }

        public async Task<CompanyProfile?> GetAsync(Guid userId)
        {
            return await _repo.GetByUserIdAsync(userId);
        }

        public async Task SaveAsync(Guid userId, CompanyProfileDto dto)
        {

            //var url = await _service.GetImageUrl();

            var profile = new CompanyProfile
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Website = dto.Website,
                LogoUrl = dto.LogoUrl
            };

            await _repo.CreateOrUpdateAsync(profile);
            _logger.LogInformation("Company profile saved for user {UserId}", userId);
        }
    }
}
