using myportfolio.DTOs;

namespace myportfolio.controllers;

using Microsoft.AspNetCore.Mvc;
using myportfolio.DTOs.BlogPost;
using myportfolio.services;

[ApiController]
[Route("api/[controller]")]
public class BlogPostsController : ControllerBase
{
    private readonly IBlogPostsService _blogPostsService;

    public BlogPostsController(IBlogPostsService blogPostsService)
    {
        _blogPostsService = blogPostsService;
    }

    // GET: api/BlogPosts
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadBlogPostDTO>), 200)]
    public async Task<ActionResult<IEnumerable<ReadBlogPostDTO>>> GetAll()
    {
        var posts = await _blogPostsService.GetAllAsync();

        return Ok(posts);
    }

    // GET: api/BlogPosts/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReadBlogPostDTO), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ReadBlogPostDTO>> GetById(int id)
    {
        var post = await _blogPostsService.GetByIdAsync(id);

        if (post == null)
            return NotFound();

        return Ok(post);
    }

    // POST: api/BlogPosts
    [HttpPost]
    [ProducesResponseType(typeof(ReadBlogPostDTO), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ReadBlogPostDTO>> Create(
        [FromBody] CreateBlogPostDTO dto)
    {
        var post = await _blogPostsService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = post.Id },
            post
        );
    }

    // PUT: api/BlogPosts/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateBlogPostDTO dto)
    {
        bool updated = await _blogPostsService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/BlogPosts/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _blogPostsService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}