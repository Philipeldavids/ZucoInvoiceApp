using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZucoInvoiceApp.Controllers
{
    using BusinessLayer.Services;
    using Microsoft.AspNetCore.Mvc;

    namespace ZucoInvoiceApp.Controllers
    {
        [ApiController]
        [Route("api/v1/[controller]")]
        public class PaystackController : ControllerBase
        {
            private readonly IPaystackService _service;
            private readonly ISubscriptionService _subService;

            public PaystackController(IPaystackService service, ISubscriptionService subService)
            {
                _service = service;
                _subService = subService;
            }
            [HttpPost("verify")]
            public async Task<IActionResult> VerifyPayment([FromBody] VerifyPaymentRequest req)
            {

                var verified = await _service.VerifyPaymentAsync(req.Reference);

                if (!verified.Status)
                    return BadRequest(new { message = "Payment not verified" });

                await _subService.SubscribeAsync(req.UserId, req.PlanName, req.Reference);
                return Ok(new { message = "Payment verified, subscription activated!" });
            }

            public class VerifyPaymentRequest
            {
                public string UserId { get; set; }
                public string PlanName { get; set; }
                public string Reference { get; set; }
            }

        }
    }

}
