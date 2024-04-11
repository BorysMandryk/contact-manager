using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactService.GetContacts();
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            using(var stream = file.OpenReadStream())
            await _contactService.ReadCsv(stream);
            var contacts = await _contactService.GetContacts();
            return CreatedAtAction(nameof(GetContacts), contacts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactRequestDto contactDto)
        {
            await _contactService.UpdateContact(id, contactDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await _contactService.DeleteContact(id);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
