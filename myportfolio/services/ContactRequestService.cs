using myportfolio.data;
using myportfolio.services;
namespace myportfolio;

using Microsoft.EntityFrameworkCore;

public class ContactRequestService : IContactRequestService
{
    private readonly PortfolioDbContext _context;

    public ContactRequestService(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateContactAsync(
        ContactFormCreateDto dto)
    {
        ContactForm contact = new ContactForm
        {
            Name = dto.Name,
            Email = dto.Email,
            Subject = dto.Subject,
            Message = dto.Message
        };

        _context.ContactForms.Add(contact);

        await _context.SaveChangesAsync();

        return contact.ID;
    }
}