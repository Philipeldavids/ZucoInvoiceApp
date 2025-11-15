using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using static BusinessLayer.Services.EmailService;
using System.Net.Mail;
using System.Net.Mime;

namespace InvoiceGenAppAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IEmailService _emailService;
        public InvoiceController(IInvoiceService invoiceService, IEmailService emailService) 
        {
            _invoiceService = invoiceService;
            _emailService = emailService;
        }

        

        [HttpGet("GetInvoiceNumber")]

        public async Task<IActionResult> GetInvoiceNumber()
        {
            try
            {
                var result = await _invoiceService.GetInvoiceNumber();
                return Ok(result);
            }
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search/{userId}/{query}")]
        public async Task<IActionResult> SearchInvoices(string query, string userId)
        {
            

            try
            {
                var filteredInvoices = await _invoiceService.SearchInvoice(query, userId);

                return Ok(filteredInvoices);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }     

        }

        [HttpGet("GetInvoiceByUser/{userId}")]

        public async Task<IActionResult> GetInvoiceByUser(string userId)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceByUser(userId);
                return Ok(invoice);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInvoiceById/{Id}")]
        public async Task<IActionResult> GetInvoiceByID(int Id)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceById(Id);

                return Ok(invoice);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("GEtInvoice")]

        public async Task<IActionResult> GetInvoiceAsync()
        {
            try
            {
                var result = await _invoiceService.GetInvoice();
                return Ok(result);
            }
            catch (Exception ex) { 
                return BadRequest ($"{ex.Message}");
            }
        }

        [HttpPost("Create")]

        public async Task<IActionResult> Create([FromForm]InvoiceDTO invoicedto)
        {
            try
            {
                var result = await _invoiceService.CreateInvoice(invoicedto);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest($"Failed to create {ex.Message}");
            }
            
        }

        [HttpPost("SendInvoice")]
        public async Task<IActionResult> SendInvoice([FromForm] IFormFile file, [FromForm] string invoiceId, [FromForm] string email, [FromForm] string businessName)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            // Save temporarily or stream directly
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var pdfBytes = stream.ToArray();
            var attachment = new EmailAttachment()
            {
                FileName = "Invoice.pdf",
                FileBytes = pdfBytes,
                ContentType = "application/pdf"
            };

            // Send via your email service (e.g., SMTP, SendGrid)
            await _emailService.SendEmailAsync(

                email,
                $"Invoice #{invoiceId}",
                $"Please find attached your invoice from {businessName}",
            attachment            

            );

            return Ok(new { message = "Invoice sent successfully" });
        }


        [HttpPatch("UpdateInvoice")]
        public async Task<IActionResult> UpdateInvoiceAsync(Invoice invoice)
        {
            try
            {
                var result = _invoiceService.UpdateInvoiceAsync(invoice);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
            

        }
        [HttpDelete("DeleteInvoice/{Id}")]

        public async Task<IActionResult> DeleteInvoiceAsync(int Id)
        {
            try
            {
                var result =await _invoiceService.DeleteInvoiceAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
