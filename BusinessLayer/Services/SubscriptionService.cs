using DataLayer.Database;
using DataLayer.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public SubscriptionService( IUnitOfWork unitOfWork, ApplicationDbContext context, UserManager<User> userManager) {

            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        //public async Task<bool> AddSubscription(Subscription subscription)
        //{
        //    var result = await _unitOfWork.SubscriptionRepository.AddSubscription(subscription);

        //    return result;
        //}

        public async Task<bool> CanGenerateInvoice(string userId)
        {
            var activeSub = await _context.Subscriptions
                .Where(s => s.UserId == userId && s.IsActive && s.EndDate > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (activeSub != null)
                return true;

            int invoiceCount = await _context.Invoices
                .CountAsync(i => i.UserId == userId);

            return invoiceCount < 5; // free limit
        }
       

        public async Task ActivatePaidPlanAsync(string userEmail, string planName, string reference)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                throw new Exception("User not found.");

            // Normalize plan name
            planName = planName.Trim().ToLower();

            // Map duration
            var durationDays = planName switch
            {
                "30 days" => 30,
                "90 days" => 90,
                "6 months" => 180,
                "1 year" => 365,
                _ => throw new Exception($"Invalid plan: {planName}")
            };

            // Deactivate old subscriptions if necessary
            var existing = await _context.Subscriptions.Where(s => s.UserId == user.Id && s.IsActive)
                .FirstOrDefaultAsync();

            if (existing == null || existing.PlanName == "Free Tier")
            {
                existing = new Subscription
                {
                    UserId = user.Id,
                    PlanName = planName,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(durationDays),
                    IsActive = true,
                    PaymentReference = reference
                };

                _context.Subscriptions.Add(existing);
            }
            else
            {
                existing.PlanName = planName;
                existing.StartDate = DateTime.UtcNow;
                existing.EndDate = DateTime.UtcNow.AddDays(durationDays);
                existing.IsActive = true;
                existing.PaymentReference = reference;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SubscribeAsync(string userId, string planName, string reference)
        {
            int days = planName switch
            {
                "30 days" => 30,
                "90 days" => 90,
                "6 months" => 180,
                "1 year" => 365,
                _ => throw new ArgumentException("Invalid plan")
            };
            var existing = await _context.Subscriptions.Where(s => s.UserId == userId)
                .FirstOrDefaultAsync();

            if (existing == null)
            {
                existing = new Subscription
                {
                    UserId = userId,
                    PlanName = planName,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                    IsActive = true,
                    PaymentReference = reference
                    //InvoicesUsed = 0,
                    //FreeLimit = 5, // free tier limit
                    //CreatedAt = DateTime.UtcNow
                };
                _context.Subscriptions.Add(existing);
            }
            else {


                existing.UserId = userId;
                existing.PlanName = planName;
                existing.StartDate = DateTime.UtcNow;
                existing.EndDate = DateTime.UtcNow.AddDays(days);
                existing.IsActive = true;
                existing.PaymentReference = reference;
               

                _context.Subscriptions.Add(existing);
            }
            
            await _context.SaveChangesAsync();
        }
    }

}
