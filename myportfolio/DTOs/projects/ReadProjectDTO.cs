namespace myportfolio.DTOs.projects;

public class ReadProjectDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Details { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();
    public string? GitHubUrl { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}