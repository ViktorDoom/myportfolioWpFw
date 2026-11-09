namespace myportfolio.DTOs.projects;
using System.ComponentModel.DataAnnotations;

public class CreateProjectDTO
{
    [Required]
    [StringLength(150)]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }
    
    public string? Details { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();

    [Url]
    public string? GitHubUrl { get; set; }

    [Url]
    public string? ImageUrl { get; set; }
}