using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLayer.Services.PaystackService;

namespace BusinessLayer.Services
{
    public interface IPaystackService
    {
        //Task<bool> VerifyPayment(string reference);

        Task<PaystackVerificationResponse> VerifyPaymentAsync(string reference);
        Task<string> InitializePaymentAsync(string userEmail, string planName);
    }
}
