namespace myportfolio.services;

public interface IContactRequestService
{
    Task<int> CreateContactAsync(ContactFormCreateDto dto);
}