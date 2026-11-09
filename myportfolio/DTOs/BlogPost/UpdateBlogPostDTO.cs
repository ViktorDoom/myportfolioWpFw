namespace myportfolio.DTOs;
using System.ComponentModel.DataAnnotations;

public class UpdateBlogPostDTO
{
    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    [Required]
    public required string Content { get; set; }
}