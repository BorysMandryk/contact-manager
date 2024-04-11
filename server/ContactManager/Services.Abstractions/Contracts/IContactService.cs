using Shared.DTO;

namespace Application.Contracts
{
    public interface IContactService
    {
        Task ReadCsv(Stream stream);
        Task<List<ContactResponseDto>> GetContacts();
        Task CreateContact(ContactRequestDto contactDto);
        Task UpdateContact(int id, ContactRequestDto contactDto);
        Task DeleteContact(int id);
    }
}
