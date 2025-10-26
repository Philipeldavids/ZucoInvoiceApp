using DataLayer.Database;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private IInvoiceRepository _invoiceRepository;
        private IItemRepository _itemRepository;
        private IContactRepository _contactRepository;
        private ISubscriptionRepository _subscriptionRepository;
        //private IUserRepository _userRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        public IInvoiceRepository InvoiceRepository => _invoiceRepository ??= new InvoiceRepository(_applicationDbContext);

        public IItemRepository ItemRepository => _itemRepository ??= new ItemRepository(_applicationDbContext);

        public IContactRepository ContactRepository => _contactRepository ??= new ContactRepository(_applicationDbContext);

        public ISubscriptionRepository SubscriptionRepository => _subscriptionRepository ??= new SubscriptionRepository(_applicationDbContext);

        //public IUserRepository UserRepository => _userRepository ??= new UserRepository(_applicationDbContext);
    }
}
