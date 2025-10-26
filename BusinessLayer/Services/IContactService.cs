using DataLayer.DTO;
using Models;

namespace BusinessLayer.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<List<Contact>> GetContactByUser(string UserId);
        Task<bool> AddContact(ContactDTO contactDTO);
        Task<bool> DeleteContact(string Id);
    }
}