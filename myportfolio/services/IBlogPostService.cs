using myportfolio.DTOs;

namespace myportfolio.services;

using myportfolio.DTOs.BlogPost;

public interface IBlogPostsService
{
    Task<IEnumerable<ReadBlogPostDTO>> GetAllAsync();

    Task<ReadBlogPostDTO?> GetByIdAsync(int id);

    Task<ReadBlogPostDTO> CreateAsync(CreateBlogPostDTO dto);

    Task<bool> UpdateAsync(int id, UpdateBlogPostDTO dto);

    Task<bool> DeleteAsync(int id);
}