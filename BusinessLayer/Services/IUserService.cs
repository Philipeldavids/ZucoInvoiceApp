using DataLayer.DTO;
using Models;
using Models.DTO;

namespace BusinessLayer.Services
{
    public interface IUserService
    {
        Task<User> AddCompany(string userid, string text);
        Task<User> GetUserByUserName(string UserName);
        Task<bool> AddUserAsync(UserRequestDTO user);

        Task<User> GetUserById(string userId);
        Task<bool> Update(UserRequestDTO userRequestDTO, string userId);

        Task<bool> DeleteUser(string userId);
        Task<List<User>> GetUser();
        Task<UserResponseDTO> Login(string email, string password);
    }
}