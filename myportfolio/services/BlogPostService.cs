using myportfolio.DTOs;

namespace myportfolio.services;

using Microsoft.EntityFrameworkCore;
using myportfolio.data;
using myportfolio.model;
using myportfolio.DTOs.BlogPost;

public class BlogPostsService : IBlogPostsService
{
    private readonly PortfolioDbContext _context;

    public BlogPostsService(PortfolioDbContext context)
    {
        _context = context;
    }

    // GET ALL
    public async Task<IEnumerable<ReadBlogPostDTO>> GetAllAsync()
    {
        return await _context.BlogPosts
            .AsNoTracking()
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new ReadBlogPostDTO
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            })
            .ToListAsync();
    }

    // GET BY ID
    public async Task<ReadBlogPostDTO?> GetByIdAsync(int id)
    {
        return await _context.BlogPosts
            .AsNoTracking()
            .Where(b => b.Id == id)
            .Select(b => new ReadBlogPostDTO
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    // CREATE
    public async Task<ReadBlogPostDTO> CreateAsync(
        CreateBlogPostDTO dto)
    {
        var blogPost = new BlogPost
        {
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow
        };

        _context.BlogPosts.Add(blogPost);

        await _context.SaveChangesAsync();

        return new ReadBlogPostDTO
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Content = blogPost.Content,
            CreatedAt = blogPost.CreatedAt,
            UpdatedAt = blogPost.UpdatedAt
        };
    }

    // UPDATE
    public async Task<bool> UpdateAsync(
        int id,
        UpdateBlogPostDTO dto)
    {
        var blogPost = await _context.BlogPosts.FindAsync(id);

        if (blogPost == null)
            return false;

        blogPost.Title = dto.Title;
        blogPost.Content = dto.Content;
        blogPost.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var blogPost = await _context.BlogPosts.FindAsync(id);

        if (blogPost == null)
            return false;

        _context.BlogPosts.Remove(blogPost);

        await _context.SaveChangesAsync();

        return true;
    }
}