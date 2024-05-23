using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService ContactService)
    {
        _contactService = ContactService;
    }
    [HttpGet]
    public async Task<IActionResult> ContactList()
    {
        var values = await _contactService.GetAllContactAsync();
        return Ok(values);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(string id)
    {
        var value = await _contactService.GetByIdContactAsync(id);
        return Ok(value);
    }
    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
    {
        await _contactService.CreateContactAsync(createContactDto);
        return Ok("Contact bilgisi eklendi.");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(string id)
    {
        await _contactService.DeleteContactAsync(id);
        return Ok("Contact bilgisi silindi.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
    {
        await _contactService.UpdateContactAsync(updateContactDto);
        return Ok("Contact bilgisi güncellendi.");
    }
}
