using BusinessLayer.Services;
using DataLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ZucoInvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;


        public ContactController(IContactService contactService)
        {
                _contactService = contactService;
        }

        [HttpGet("GetAllContacts")]

        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var contacts = _contactService.GetAll();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetContactByUser/{userId}")]

        public async Task<IActionResult> GetContactByUser(string userId)
        {
            try
            { 
                var result = await _contactService.GetContactByUser(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddContact")]

        public async Task<IActionResult> AddContact([FromBody] ContactDTO contact)
        {
            try
            {
                var result = await _contactService.AddContact(contact);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{Id}")]

        public async Task<IActionResult> DeleteContact(string Id)
        {
            try
            {
                var result = await _contactService.DeleteContact(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
