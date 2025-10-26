using DataLayer.Database;
using DataLayer.Interface;
using DataLayer.Repository;
using Microsoft.AspNetCore.Http;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public InvoiceService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService) 
        { 
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        //public async Task<string> UploadFile(IFormFile file)
        //{
        //    if (file == null) throw new ArgumentNullException(nameof(file));

        //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", };
        //    var fileExtension = Path.GetExtension(file.FileName)?.ToLower();

        //    if (!allowedExtensions.Contains(fileExtension))
        //    {
        //        throw new ArgumentException("Upload file with correct extension");
        //    }

        //    var fileName = $"{Guid.NewGuid()}{fileExtension}";
        //    var filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        //    // Ensure the directory exists
        //    var directory = Path.GetDirectoryName(filePath);
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //    // Return the file URL
        //    var fileUrl = $"{directory}/{fileName}";

        //    return fileUrl;

        //}

        public async Task<List<Invoice>> SearchInvoice(string query, string userId)
        {
            var result = await _unitOfWork.InvoiceRepository.SearchInvoice(query, userId);
            return result;
        }
        public async Task<Invoice> CreateInvoice(InvoiceDTO invoicedto)
        {
            var invoicenum = invoicedto.InvoiceNumber.Substring(4);
            
            Invoice invoice = new Invoice();
            var itemList = JsonConvert.DeserializeObject<List<Item>>(invoicedto.Items);
            try
            {
                if(invoicedto.Image != null)
                {
                    var getImageUrl = await _cloudinaryService.UploadFileAsync(invoicedto.Image);
                    invoice.ImageURl = getImageUrl.SecureUrl.ToString();
                }
               
                
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
       
            invoice.InvoiceNumber = int.Parse(invoicenum);
            invoice.Client = invoicedto.ContactName;
            invoice.CreatedDate = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd");
            invoice.TotalPrice = invoicedto.TotalPrice;
            invoice.Items = itemList;
            invoice.FootNote = invoicedto.FootNote;
            invoice.Tax = invoicedto.Tax;
            invoice.UserId = invoicedto.UserId;



            if(await _unitOfWork.InvoiceRepository.CreateInvoice(invoice))
            {
                return await GetInvoiceById(invoice.InvoiceID);
            }
            return null;
        }

        public async Task<List<Invoice>> GetInvoiceByUser(string userId)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetInvoiceByUser(userId);
            return invoice;
        }
        public async Task<Invoice> GetInvoiceById(int Id)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetInvoiceByIdAsync(Id);

            return invoice;
        }
        public async Task<List<Invoice>> GetInvoice()
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetInvoice();
            return invoice;
        }
        public async Task<bool> DeleteInvoiceAsync(int Id)
        {
            var result = await _unitOfWork.InvoiceRepository.DeleteInvoice(Id);
            return result;
        }
        public async Task<bool> UpdateInvoiceAsync(Invoice invoice)
        {
            var result = await _unitOfWork.InvoiceRepository.UpdateInvoice(invoice);
            return result;
        }

        public async Task<int?> GetInvoiceNumber()
        {
            var result = await _unitOfWork.InvoiceRepository.GetInvoiceNumber();

            return result;
        }
    }
}
