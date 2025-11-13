using Models;

namespace DataLayer.Repository
{
    public interface IContactRepository
    {
        Task<bool> EditContact(Contact contact);
        Task<Contact> GetContactById(string Id);
        Task<IEnumerable<Contact>> GetAll();
        Task<List<Contact>> GetContactByUser(string UserId);
        Task<bool> AddContact(Contact contact);
        Task<bool> DeleteContact(string Id);
    }
}