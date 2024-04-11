using Application.Contracts;
using CsvHelper;
using Data.Models;
using Infrastructure.Models;
using Shared.DTO;
using System.Globalization;

namespace Infrastructure.Services
{
    public class ContactCsvService : IContactFileService
    {
        public IEnumerable<ContactRequestDto> ReadFile(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ContactCsvModel>().ToList();
                var contacts = new List<ContactRequestDto>();
                foreach (var record in records)
                {
                    contacts.Add(new ContactRequestDto
                    {
                        Name = record.Name,
                        DateOfBirth = record.DateOfBirth,
                        Married = record.Married,
                        Phone = record.Phone,
                        Salary = record.Salary
                    });
                }
                return contacts;
            }
        }
    }
}
