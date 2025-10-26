using BusinessLayer.Services;
using DataLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace InvoiceGenAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public AuthController(IUserService userService, UserManager<User> userManager, IEmailService emailService)
        {
            _userService = userService;
            _userManager = userManager;

        }

        [HttpPost("Login")]
            
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO loginRequest)
        {
            var result = await _userService.Login(loginRequest.email, loginRequest.password);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest("User does not Exist, Please use the SignUp Link");

        }

        [HttpPost("AddUser")]

        public async Task<IActionResult> AddUser([FromBody]UserRequestDTO user)
        {
            var result = await _userService.AddUserAsync(user);

            return Ok(result);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://yourfrontend.com/reset-password?email={dto.Email}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendEmailAsync(dto.Email, "Reset Password",
                $"Click the link below to reset your password:<br/><a href='{resetLink}'>Reset Password</a>");

            return Ok("Password reset link sent successfully");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("User not found");

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password reset successful");
        }
    }

    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}

