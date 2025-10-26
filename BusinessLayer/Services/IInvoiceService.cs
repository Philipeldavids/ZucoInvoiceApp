using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> SearchInvoice(string query, string userId);
        Task<List<Invoice>> GetInvoiceByUser(string userId);
        Task<int?> GetInvoiceNumber();
        Task<Invoice> CreateInvoice(InvoiceDTO invoicedto);
        Task<Invoice> GetInvoiceById(int Id);
        Task<bool> UpdateInvoiceAsync(Invoice invoice);
        Task<bool> DeleteInvoiceAsync(int Id);
        Task<List<Invoice>> GetInvoice();
    }
}
