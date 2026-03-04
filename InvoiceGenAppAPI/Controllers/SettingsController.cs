using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using System.Security.Claims;


namespace ZucoInvoiceApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class SettingsController : ControllerBase
    {

            private readonly CompanyProfileService _service;

            public SettingsController(CompanyProfileService service)
            {
                _service = service;
            }

            [HttpGet("getcompany")]
             
        public async Task<IActionResult> Get()
           {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await _service.GetAsync(userId);
                return Ok(result);
            }

         [HttpPost("savecompany")]
        
        public async Task<IActionResult> Save(CompanyProfileDto dto)
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _service.SaveAsync(userId, dto);
                return Ok(new { message = "Company settings saved successfully" });
            }
        
    }
}
