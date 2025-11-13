using DataLayer.DTO;
using Models;
using Models.DTO;

namespace BusinessLayer.Services
{
    public interface IContactService
    {
        Task<bool> EditContact(string Id, ContactDTO contact);
        Task<Contact> GetContactById(string Id);
        Task<IEnumerable<Contact>> GetAll();
        Task<List<Contact>> GetContactByUser(string UserId);
        Task<bool> AddContact(ContactDTO contactDTO);
        Task<bool> DeleteContact(string Id);
    }
}