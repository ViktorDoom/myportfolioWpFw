namespace myportfolio.DTOs.ContactRequest;

public class ContactRequestCreatedDTO
{
    public int Id { get; set; }

    public ContactRequestCreatedDTO(int id)
    {
        Id = id;
    }
}