namespace myportfolio.controllers;

using myportfolio.services;
using myportfolio.DTOs.ContactRequest;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRequestService _contactService;

    public ContactController(IContactRequestService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactRequestCreatedDTO), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ContactRequestCreatedDTO>> CreateContact(
        [FromBody] ContactFormCreateDto dto)
    {
        int id = await _contactService.CreateContactAsync(dto);

        return StatusCode(201, new ContactRequestCreatedDTO(id));
    }
}