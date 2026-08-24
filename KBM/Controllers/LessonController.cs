using Application.Features.Lesson.DTOs;
using Application.Features.Lesson.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KBM.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LessonController : ControllerBase
{
    private readonly LessonService _service;
    public LessonController(LessonService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLessonDto dto)
    {
        var created = await _service.CreateAsync(dto);
        if (created is null)
            return BadRequest("DepartmentID, FunctionID, or IndustryID does not reference an existing record.");

        return CreatedAtAction(nameof(Get), new { id = created.LessonID }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateLessonDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result switch
        {
            null => BadRequest("DepartmentID, FunctionID, or IndustryID does not reference an existing record."),
            false => NotFound(),
            true => NoContent()
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();
}