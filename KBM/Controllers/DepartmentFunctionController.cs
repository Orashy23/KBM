using Application.Features.DepartmentFunction.DTOs;
using Application.Features.DepartmentFunction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KBM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentFunctionController : ControllerBase
{
    private readonly DepartmentFunctionService _service;
    public DepartmentFunctionController(DepartmentFunctionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{departmentId:int}/{functionId:int}")]
    public async Task<IActionResult> Get(int departmentId, int functionId)
    {
        var result = await _service.GetByIdAsync(departmentId, functionId);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentFunctionDto dto)
    {
        var created = await _service.CreateAsync(dto);
        if (created is null)
            return BadRequest("Invalid DepartmentID/FunctionID, or this link already exists.");

        return CreatedAtAction(nameof(Get),
            new { departmentId = created.DepartmentID, functionId = created.FunctionID }, created);
    }

    [HttpDelete("{departmentId:int}/{functionId:int}")]
    public async Task<IActionResult> Delete(int departmentId, int functionId) =>
        await _service.DeleteAsync(departmentId, functionId) ? NoContent() : NotFound();
}