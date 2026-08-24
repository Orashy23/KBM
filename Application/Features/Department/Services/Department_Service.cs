// Application/Features/Department/Services/Department_Service.cs
using Application.Features.Department.DTOs;
using Domain.Entities;
using Infrastructure;

public class Department_Service
{
    private readonly AppDbContext _context;
    public Department_Service(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Department_DTO>> GetAllAsync() =>
        await _context.Department
            .Select(d => new Department_DTO { Id = d.Id, Name = d.Name, CreatedDate = d.CreatedDate, ModifiedDate = d.ModifiedDate })
            .ToListAsync();

    public async Task<DepartmentDTO?> GetByIdAsync(int id)
    {
        var d = await _context.Departments.FindAsync(id);
        return d == null ? null : new DepartmentDTO { Id = d.Id, Name = d.Name, CreatedDate = d.CreatedDate, ModifiedDate = d.ModifiedDate };
    }

    public async Task<DepartmentDTO> CreateAsync(DepartmentDTO dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        dto.Id = department.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, DepartmentDTO dto)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return false;

        department.Name = dto.Name;
        department.ModifiedDate = DateTime.UtcNow;
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