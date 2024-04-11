using Data.Models;

namespace Data.Contracts
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContactsAsync();
        Task<Contact?> GetContactAsync(int id);
        Task AddContactAsync(Contact contact);
        Task AddContactsAsync(IEnumerable<Contact> contacts);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(Contact contact);
    }
}
