using DataLayer.Database;
using DataLayer.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    { 
        private readonly ApplicationDbContext _context;
        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSubscription(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
           if(_context.SaveChanges() > 0)
            {
                return true;
            }
           return false;
                       
        }
    }
}
