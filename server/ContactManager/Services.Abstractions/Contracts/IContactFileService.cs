using Shared.DTO;

namespace Application.Contracts
{
    public interface IContactFileService
    {
        IEnumerable<ContactRequestDto> ReadFile(Stream stream);
    }
}
