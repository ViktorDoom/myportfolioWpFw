using System.ComponentModel.DataAnnotations;

public class ContactForm
{
    [Key]
    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}