using DataLayer.Database;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> SearchInvoice(string query, string userId);
        Task<List<Invoice>> GetInvoiceByUser(string userId);
        Task<Invoice> GetInvoiceByIdAsync(int Id);
        Task<bool> CreateInvoice(Invoice invoice);
        Task<List<Invoice>> GetInvoice();

        Task<bool> UpdateInvoice(Invoice invoice);
        Task<bool> DeleteInvoice(int Id);
        Task<int?> GetInvoiceNumber();

    }
}
