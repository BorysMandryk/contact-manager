using Data.Contracts;
using Data.DataAccess;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _dbContext;

        public ContactRepository(ContactContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddContactAsync(Contact contact)
        {
            _dbContext.Add(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddContactsAsync(IEnumerable<Contact> contacts)
        {
            _dbContext.Contacts.AddRange(contacts);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Contact contact)
        {
            _dbContext.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Contact>> GetAllContactsAsync()
        {
            return _dbContext.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetContactAsync(int id)
        {
            return await _dbContext.Contacts.FindAsync(id);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _dbContext.Update(contact);
            await _dbContext.SaveChangesAsync();
        }
    }
}
