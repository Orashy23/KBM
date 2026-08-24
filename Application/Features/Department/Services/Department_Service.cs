using Application.Features.Department.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using DepartmentEntity = Domain.Entities.Department;

namespace Application.Features.Department.Services;

public class DepartmentService
{
    private readonly AppDbContext _context;
    public DepartmentService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync() =>
        await _context.Departments
            .Select(d => new DepartmentDto
            {
                DepartmentID = d.DepartmentID,
                DepartmentName = d.DepartmentName,
                CreatedDate = d.CreatedDate,
                UpdatedDate = d.UpdatedDate
            })
            .ToListAsync();

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return null;

        return new DepartmentDto
        {
            DepartmentID = department.DepartmentID,
            DepartmentName = department.DepartmentName,
            CreatedDate = department.CreatedDate,
            UpdatedDate = department.UpdatedDate
        };
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto)
    {
        var department = new DepartmentEntity
        {
            DepartmentName = dto.DepartmentName,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return new DepartmentDto
        {
            DepartmentID = department.DepartmentID,
            DepartmentName = department.DepartmentName,
            CreatedDate = department.CreatedDate,
            UpdatedDate = department.UpdatedDate
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateDepartmentDto dto)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return false;

        department.DepartmentName = dto.DepartmentName;
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