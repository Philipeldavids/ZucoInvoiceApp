using Models;

namespace BusinessLayer.Services
{
    public interface ISubscriptionService
    {
        //Task<bool> AddSubscription(Subscription subscription);

        Task<bool> CanGenerateInvoice(string userId);

        Task SubscribeAsync(string userId, string planName, string reference);

        Task ActivatePaidPlanAsync(string userEmail, string planName, string reference);
    }
}