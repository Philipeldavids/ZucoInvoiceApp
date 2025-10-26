using DataLayer.Database;
using DataLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Formats.Asn1.AsnWriter;

namespace DataLayer.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public InvoiceRepository(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<List<Invoice>> SearchInvoice(string query, string userId)
        {
            var filteredInvoices = _applicationDbContext.Invoices
            .Where(invoice => invoice.UserId == userId && (
                   Convert.ToString(invoice.InvoiceNumber).Contains(query.ToLower()) ||
                   invoice.Client.ToLower().StartsWith(query.ToLower())))
               .ToList();

            return filteredInvoices;
        }
        public async Task<List<Invoice>> GetInvoiceByUser(string userId)
        {
            var invoice = _applicationDbContext.Invoices
                        .Where(x => x.UserId == userId)                
                        .Include(x => x.Items).
                        ToList();

            return invoice;
        }
    
        public async Task<Invoice> GetInvoiceByIdAsync(int Id)
        {
            var invoice = _applicationDbContext.Invoices
                         .Where(x => x.InvoiceID == Id)
                         .Include(x => x.Items)
                         .FirstOrDefault();

            return invoice;
        }
        public async Task<List<Invoice>> GetInvoice()
        {
            List<Invoice> items = new List<Invoice>();

            var data = from item in _applicationDbContext.Invoices
                       select new Invoice()
                       {
                           InvoiceID = item.InvoiceID,
                           Client = item.Client,
                           CreatedDate = item.CreatedDate,                          
                           Tax = item.Tax,
                           FootNote = item.FootNote,
                           TotalPrice = item.TotalPrice,
                           Items = item.Items,
                       };
            return data.ToList();
        }
        public async Task<bool> CreateInvoice(Invoice invoice)
        {
            _applicationDbContext.Invoices.Add(invoice);
            var result = await _applicationDbContext.SaveChangesAsync();
            
            return result > 0;
        }

        public async Task<bool> DeleteInvoice(int Id)
        {
            var invoice = await _applicationDbContext.Invoices.Where(inv => inv.InvoiceID == Id).FirstOrDefaultAsync();
            if (invoice == null)
            {
                return false;
            }
            
            _applicationDbContext.Invoices.Remove(invoice);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateInvoice(Invoice invoice)
        {
            var nvoice = await _applicationDbContext.Invoices.FirstOrDefaultAsync(invoic => invoic.InvoiceID == invoice.InvoiceID);
            if (nvoice == null){
                return false;
            }
            _applicationDbContext.Invoices.Update(nvoice);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0;
        }


        public async Task<int?> GetInvoiceNumber()
        {
            var invoiceNum = 10000;
            var invoiceNo =  _applicationDbContext.Invoices.OrderBy(x => x.InvoiceNumber).Select(x => x.InvoiceNumber).LastOrDefault();

            if(invoiceNo == null || invoiceNo < invoiceNum)
            {
                return invoiceNum;
            }
            return invoiceNo;
        }
    }


}
