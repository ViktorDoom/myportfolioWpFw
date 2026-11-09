namespace myportfolio.controllers;

using Microsoft.AspNetCore.Mvc;
using myportfolio.DTOs.projects;
using myportfolio.services;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    // GET: api/projects
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadProjectDTO>), 200)]
    public async Task<ActionResult<IEnumerable<ReadProjectDTO>>> GetAll()
    {
        var projects = await _projectsService.GetAllAsync();

        return Ok(projects);
    }

    // GET: api/projects/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReadProjectDTO), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ReadProjectDTO>> GetById(int id)
    {
        var project = await _projectsService.GetByIdAsync(id);

        if (project == null)
            return NotFound();

        return Ok(project);
    }

    // POST: api/projects
    [HttpPost]
    [ProducesResponseType(typeof(ReadProjectDTO), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ReadProjectDTO>> Create(
        [FromBody] CreateProjectDTO dto)
    {
        var project = await _projectsService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = project.Id },
            project
        );
    }

    // PUT: api/projects/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateProjectDTO dto)
    {
        bool updated = await _projectsService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/projects/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _projectsService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}