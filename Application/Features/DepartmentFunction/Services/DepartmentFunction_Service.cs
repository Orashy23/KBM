using Application.Features.DepartmentFunction.DTOs;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using DepartmentFunctionEntity = Domain.Entities.DepartmentFunction;

namespace Application.Features.DepartmentFunction.Services;

public class DepartmentFunctionService
{
    private readonly AppDbContext _context;
    public DepartmentFunctionService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<DepartmentFunctionDto>> GetAllAsync() =>
        await _context.DepartmentFunctions
            .ProjectToType<DepartmentFunctionDto>()
            .ToListAsync();

    public async Task<DepartmentFunctionDto?> GetByIdAsync(int departmentId, int functionId) =>
        await _context.DepartmentFunctions
            .Where(df => df.DepartmentID == departmentId && df.FunctionID == functionId)
            .ProjectToType<DepartmentFunctionDto>()
            .FirstOrDefaultAsync();

    public async Task<DepartmentFunctionDto?> CreateAsync(CreateDepartmentFunctionDto dto)
    {
        var departmentExists = await _context.Departments.AnyAsync(d => d.DepartmentID == dto.DepartmentID);
        var functionExists = await _context.Functions.AnyAsync(f => f.FunctionID == dto.FunctionID);
        if (!departmentExists || !functionExists) return null;

        var alreadyLinked = await _context.DepartmentFunctions
            .AnyAsync(df => df.DepartmentID == dto.DepartmentID && df.FunctionID == dto.FunctionID);
        if (alreadyLinked) return null;

        var link = dto.Adapt<DepartmentFunctionEntity>();

        _context.DepartmentFunctions.Add(link);
        await _context.SaveChangesAsync();

        // Re-read so the related names are populated on the returned DTO.
        return await GetByIdAsync(link.DepartmentID, link.FunctionID);
    }

    public async Task<bool> DeleteAsync(int departmentId, int functionId)
    {
        var link = await _context.DepartmentFunctions
            .FirstOrDefaultAsync(df => df.DepartmentID == departmentId && df.FunctionID == functionId);
        if (link == null) return false;

        _context.DepartmentFunctions.Remove(link);
        await _context.SaveChangesAsync();
        return true;
    }
}
