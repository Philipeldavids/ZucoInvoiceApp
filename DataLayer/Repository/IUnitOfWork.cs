using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IUnitOfWork
    {
        public IInvoiceRepository InvoiceRepository { get; }

        public IItemRepository ItemRepository { get; }  

        public IContactRepository ContactRepository { get; }

        public ISubscriptionRepository SubscriptionRepository { get; }
    }
}
