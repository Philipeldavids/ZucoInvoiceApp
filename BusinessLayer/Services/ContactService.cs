using DataLayer.DTO;
using DataLayer.Repository;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserService _userService;
        public ContactService(IContactRepository contactRepository, IUserService userService) { 
        _contactRepository = contactRepository;
            _userService = userService;
        }
        public  async Task<IEnumerable<Contact>> GetAll()
        {
            return await _contactRepository.GetAll();
        }
        public async Task<List<Contact>> GetContactByUser(string UserId)
        {
            var contact = await _contactRepository.GetContactByUser(UserId);

            return contact;

        }

        public async Task<bool> EditContact(string Id, ContactDTO contact)
        {

            var contct = await GetContactById(Id);
           

            contct.CustomerAddress = contact.CustomerAddress;
            contct.CustomerEmail = contact.CustomerEmail;
            contct.CustomerName = contact.CustomerName; 
            contct.CustomerPhoneNumber = contact.CustomerPhoneNumber;

            return await _contactRepository.EditContact(contct);
        }

        public async Task<Contact> GetContactById(string Id)
        {
            return await _contactRepository.GetContactById(Id);
        }

        public async Task<bool> DeleteContact(string Id)
        {
            await _contactRepository.DeleteContact(Id);
            return true;
        }
        public async Task<bool> AddContact(ContactDTO contactDTO)
        {
            //var user = await _userService.GetUserById(contactDTO.UserId);
            Contact contact = new Contact();

            contact.CustomerAddress = contactDTO.CustomerAddress;
            contact.CustomerPhoneNumber = contactDTO.CustomerPhoneNumber;
            contact.CustomerName   = contactDTO.CustomerName;
            contact.CustomerEmail = contactDTO.CustomerEmail;            
            contact.UserId = contactDTO.UserId;
            
            
           var result = await _contactRepository.AddContact(contact);
           return result;
        }
    }
}
