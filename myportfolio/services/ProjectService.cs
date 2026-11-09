namespace myportfolio.services;
using Microsoft.EntityFrameworkCore;
using myportfolio.data;
using myportfolio.model;
using myportfolio.DTOs.projects;

public class ProjectService: IProjectsService
{
        private readonly PortfolioDbContext _context;

    public ProjectService(PortfolioDbContext context)
    {
        _context = context;
    }

    // GET ALL
    public async Task<IEnumerable<ReadProjectDTO>> GetAllAsync()
    {
        return await _context.Projects
            .AsNoTracking()
            .Select(p => new ReadProjectDTO
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Details =  p.Details,
                Technologies = p.Technologies,
                GitHubUrl = p.GitHubUrl,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    // GET BY ID
    public async Task<ReadProjectDTO?> GetByIdAsync(int id)
    {
        return await _context.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ReadProjectDTO
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Details =  p.Details,
                Technologies = p.Technologies,
                GitHubUrl = p.GitHubUrl,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    // CREATE
    public async Task<ReadProjectDTO> CreateAsync(
        CreateProjectDTO dto)
    {
        var project = new Project
        {
            Title = dto.Title,
            Description = dto.Description,
            Details =  dto.Details,
            Technologies = dto.Technologies,
            GitHubUrl = dto.GitHubUrl,
            ImageUrl = dto.ImageUrl,
            CreatedAt = DateTime.UtcNow
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ReadProjectDTO
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Details =  project.Details,
            Technologies = project.Technologies,
            GitHubUrl = project.GitHubUrl,
            ImageUrl = project.ImageUrl,
            CreatedAt = project.CreatedAt
        };
    }

    // UPDATE
    public async Task<bool> UpdateAsync(
        int id, UpdateProjectDTO dto)
    {
        var project = await _context.Projects
            .FindAsync(id);

        if (project == null)
            return false;

        project.Title = dto.Title;
        project.Description = dto.Description;
        project.Details = dto.Details;
        project.Technologies = dto.Technologies;
        project.GitHubUrl = dto.GitHubUrl;
        project.ImageUrl = dto.ImageUrl;

        await _context.SaveChangesAsync();

        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _context.Projects
            .FindAsync(id);

        if (project == null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return true;
    }
    
}