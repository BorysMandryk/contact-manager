using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
    }
}
