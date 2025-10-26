using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface ISubscriptionRepository
    {
        Task<bool> AddSubscription(Subscription subscription);
    }
}
