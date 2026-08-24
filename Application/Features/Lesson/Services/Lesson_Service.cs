using Application.Features.Lesson.DTOs;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using LessonEntity = Domain.Entities.Lesson;

namespace Application.Features.Lesson.Services;

public class LessonService
{
    private readonly AppDbContext _context;
    public LessonService(AppDbContext context) => _context = context;

    // ProjectToType flattens Function/Department/Industry names into the DTO as
    // LEFT JOINs, so these reads are a single SQL query with no Include needed.
    public async Task<IEnumerable<LessonDto>> GetAllAsync() =>
        await _context.Lessons
            .ProjectToType<LessonDto>()
            .ToListAsync();

    public async Task<LessonDto?> GetByIdAsync(int id) =>
        await _context.Lessons
            .Where(l => l.LessonID == id)
            .ProjectToType<LessonDto>()
            .FirstOrDefaultAsync();

    public async Task<LessonDto?> CreateAsync(CreateLessonDto dto)
    {
        if (!await ForeignKeysExistAsync(dto.DepartmentID, dto.FunctionID, dto.IndustryID))
            return null;

        var lesson = dto.Adapt<LessonEntity>();
        lesson.ModifiedDate = DateTime.UtcNow;

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        // Re-read so the related names are populated on the returned DTO.
        return await GetByIdAsync(lesson.LessonID);
    }

    // null = invalid FK, false = not found, true = success
    public async Task<bool?> UpdateAsync(int id, UpdateLessonDto dto)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null) return false;

        if (!await ForeignKeysExistAsync(dto.DepartmentID, dto.FunctionID, dto.IndustryID))
            return null;

        dto.Adapt(lesson);
        lesson.ModifiedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null) return false;

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ForeignKeysExistAsync(int departmentId, int functionId, int industryId) =>
        await _context.Departments.AnyAsync(d => d.DepartmentID == departmentId) &&
        await _context.Functions.AnyAsync(f => f.FunctionID == functionId) &&
        await _context.Industries.AnyAsync(i => i.IndustryID == industryId);
}
