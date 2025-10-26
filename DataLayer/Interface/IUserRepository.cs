using DataLayer.DTO;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface IUserRepository
    {
        Task<User> AddCompany(string userId, string text);
        
        Task<User> GetUserByUserName(string UserName);
        Task<bool> AddUser(User user);
        Task<User> GetUserById(string userId);
        Task<UserResponseDTO> Login(string email, string password);
        Task<bool> Update(UserRequestDTO userRequestDTO, string userId);
        Task<bool> DeleteUser(string userId);

        Task<List<User>> GetUser();
    }
}
