using DataLayer.DTO;
using DataLayer.Interface;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IEmailService emailService) 
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<List<User>> GetUser()
        {
            var user = await _userRepository.GetUser();
            return user;
        }
        public async Task<bool> AddUserAsync(UserRequestDTO user)
        {
            bool result = false;
            
            if(user.Password == user.ConfirmPassword)
            {
                User usr = new User();
                usr.Password = user.Password;
                usr.PhoneNumber = user.PhoneNumber;
                usr.Email = user.Email;
                usr.UserName = user.Email;

                result = await _userRepository.AddUser(usr);                
            }
            await _emailService.SendEmailAsync(
        user.Email,
        "Welcome to ZucoInvoice!",
        $@"<h2>Welcome {user.Email}!</h2>
            <p>Your ZucoInvoice account has been created successfully.</p>
            <p>You can now start creating invoices, tracking payments, and managing your clients.</p>
            <br />
            <strong>Thank you for choosing ZucoInvoice 🚀</strong>"
    );
            return result;
        }

       
        public async Task<User> AddCompany(string userid, string text)
        {
            var result = await _userRepository.AddCompany(userid, text);

            return result;
        }
        public async Task<User> GetUserByUserName (string UserName)
        {
            var result = await _userRepository.GetUserByUserName(UserName);
            return result;
        }
        public async Task<User> GetUserById(string userId)
        {
            var result = await _userRepository.GetUserById(userId);
            return result;
        }
        public async Task<UserResponseDTO> Login(string email, string password)
        {
            var token = await _userRepository.Login(email, password);
            return token;
        }

        public async Task<bool> Update(UserRequestDTO userRequestDTO, string userId)
        {
            var result = await _userRepository.Update(userRequestDTO, userId);
            return result;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var result = await _userRepository.DeleteUser(userId);
            return result;
        }
    }

}
