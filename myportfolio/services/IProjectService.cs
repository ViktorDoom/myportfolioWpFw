namespace myportfolio.services;

using myportfolio.DTOs.projects;

public interface IProjectsService
{
    Task<IEnumerable<ReadProjectDTO>> GetAllAsync();

    Task<ReadProjectDTO?> GetByIdAsync(int id);

    Task<ReadProjectDTO> CreateAsync(CreateProjectDTO dto);

    Task<bool> UpdateAsync(int id, UpdateProjectDTO dto);

    Task<bool> DeleteAsync(int id);
}