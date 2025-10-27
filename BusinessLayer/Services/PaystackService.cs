using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PaystackService : IPaystackService
    {
        private readonly HttpClient _client;
        private readonly string _secretKey;
        private readonly IConfiguration _config;

        public PaystackService(IConfiguration config)
        {
            _config = config;
            _secretKey = config["Paystack:SecretKey"];
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.paystack.co/")
            };
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _secretKey);
        }

        //public async Task<bool> VerifyPayment(string reference)
        //{
        //    var response = await _client.GetAsync($"transaction/verify/{reference}");
        //    if (!response.IsSuccessStatusCode)
        //        return false;

        //    var json = await response.Content.ReadAsStringAsync();
        //    using var doc = JsonDocument.Parse(json);
        //    var data = doc.RootElement.GetProperty("data");
        //    string status = data.GetProperty("status").GetString();

        //    return status == "success";
        //}

        public class PaystackVerificationResponse
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public string PlanName { get; set; }
            public string CustomerEmail { get; set; }
        }
        public async Task<PaystackVerificationResponse> VerifyPaymentAsync(string reference)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.paystack.co/transaction/verify/{reference}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config["Paystack:SecretKey"]);

            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            if (!response.IsSuccessStatusCode)
                return new PaystackVerificationResponse { Status = false, Message = "Verification failed" };

            var json = JsonDocument.Parse(content);
            var data = json.RootElement.GetProperty("data");

            string planName = "30 days"; // default fallback
            if (data.TryGetProperty("metadata", out var meta))
            {
                if (meta.TryGetProperty("plan", out var planProp))
                    planName = planProp.GetString();
            }

            return new PaystackVerificationResponse
            {
                Status = data.GetProperty("status").GetString() == "success",
                Message = "Payment verified successfully",
                CustomerEmail = data.GetProperty("customer").GetProperty("email").GetString(),
                PlanName = planName
            };
        }
        public async Task<string> InitializePaymentAsync(string userEmail, string planName)
        {
            var plans = new Dictionary<string, int>
            {
                { "30 days", 5000 },
                { "90 days", 10000 },
                { "6 months", 20000 },
                { "1 year", 30000 },
            };

            double amount = plans[planName] * 100; // Paystack expects kobo
            var payload = new
            {
                
                email = userEmail,
                amount = amount,
                callback_url = $"{_config["AppUrl"]}/api/v1/subscription/verify",
                metadata = new
                {
                    plan = planName,
                    custom_fields = new[]
                    {
                        new
                        {
                            display_name = "Subscription Plan",
                            variable_name = "plan",
                            value = planName
                        }
                    }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.paystack.co/transaction/initialize");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config["Paystack:SecretKey"]);
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Paystack Init Error: {content}");

            var data = JsonSerializer.Deserialize<JsonElement>(content);
            return data.GetProperty("data").GetProperty("authorization_url").GetString();
        }
    }

}
