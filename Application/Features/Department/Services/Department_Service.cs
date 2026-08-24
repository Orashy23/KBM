using Application.Features.Department.DTOs;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using DepartmentEntity = Domain.Entities.Department;

namespace Application.Features.Department.Services;

public class DepartmentService
{
    private readonly AppDbContext _context;
    public DepartmentService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync() =>
        await _context.Departments
            .ProjectToType<DepartmentDto>()
            .ToListAsync();

    public async Task<DepartmentDto?> GetByIdAsync(int id) =>
        await _context.Departments
            .Where(d => d.DepartmentID == id)
            .ProjectToType<DepartmentDto>()
            .FirstOrDefaultAsync();

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto)
    {
        var department = dto.Adapt<DepartmentEntity>();

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return department.Adapt<DepartmentDto>();
    }

    public async Task<bool> UpdateAsync(int id, UpdateDepartmentDto dto)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return false;

        dto.Adapt(department);
        department.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return false;

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return true;
    }
}
