using Application.Contracts;
using Data.Contracts;
using Data.Models;
using Shared.DTO;
using Shared.Exceptions;

namespace Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IContactFileService _fileService;

        public ContactService(IContactRepository repository, IContactFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task CreateContact(ContactRequestDto contactDto)
        {
            var contact = new Contact
            {
                Name = contactDto.Name,
                DateOfBirth = contactDto.DateOfBirth,
                Married = contactDto.Married,
                Phone = contactDto.Phone,
                Salary = contactDto.Salary
            };
            await _repository.AddContactAsync(contact);
        }

        public async Task DeleteContact(int id)
        {
            var contact = await _repository.GetContactAsync(id);
            if (contact == null)
            {
                throw new EntityNotFoundException($"Entity with id {id} is not found");
            }
            await _repository.DeleteContactAsync(contact);
        }

        public async Task<List<ContactResponseDto>> GetContacts()
        {
            var contacts = await _repository.GetAllContactsAsync();

            var contactDtoList = new List<ContactResponseDto>();
            foreach (var contact in contacts)
            {
                contactDtoList.Add(new ContactResponseDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    DateOfBirth = contact.DateOfBirth,
                    Married = contact.Married,
                    Phone = contact.Phone,
                    Salary = contact.Salary
                });
            }

            return contactDtoList;
        }

        public async Task ReadCsv(Stream stream)
        {
            var contactDtos = _fileService.ReadFile(stream);
            var contacts = new List<Contact>();
            foreach (var contactDto in contactDtos)
            {
                contacts.Add(new Contact
                {
                    Name = contactDto.Name,
                    DateOfBirth = contactDto.DateOfBirth,
                    Married = contactDto.Married,
                    Phone = contactDto.Phone,
                    Salary = contactDto.Salary
                });
            }
            await _repository.AddContactsAsync(contacts);
        }

        public async Task UpdateContact(int id, ContactRequestDto contactDto)
        {
            var contact = await _repository.GetContactAsync(id);
            if (contact == null)
            {
                throw new EntityNotFoundException($"Entity with id {id} is not found");
            }

            contact.Name = contactDto.Name;
            contact.DateOfBirth = contactDto.DateOfBirth;
            contact.Married = contactDto.Married;
            contact.Phone = contactDto.Phone;
            contact.Salary = contactDto.Salary;

            await _repository.UpdateContactAsync(contact);
        }
    }
}
