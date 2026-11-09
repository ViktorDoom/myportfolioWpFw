namespace myportfolio.DTOs.BlogPost;
using System.ComponentModel.DataAnnotations;

public class CreateBlogPostDTO
{
    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    [Required]
    public required string Content { get; set; }

}