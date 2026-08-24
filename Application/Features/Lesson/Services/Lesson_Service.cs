using Application.Features.Lesson.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using LessonEntity = Domain.Entities.Lesson;

namespace Application.Features.Lesson.Services;

public class LessonService
{
    private readonly AppDbContext _context;
    public LessonService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<LessonDto>> GetAllAsync()
    {
        var lessons = await _context.Lessons
            .Include(l => l.Function)
            .Include(l => l.Department)
            .Include(l => l.Industry)
            .ToListAsync();

        return lessons.Select(ToDto);
    }

    public async Task<LessonDto?> GetByIdAsync(int id)
    {
        var lesson = await _context.Lessons
            .Include(l => l.Function)
            .Include(l => l.Department)
            .Include(l => l.Industry)
            .FirstOrDefaultAsync(l => l.LessonID == id);

        return lesson is null ? null : ToDto(lesson);
    }

    public async Task<LessonDto?> CreateAsync(CreateLessonDto dto)
    {
        if (!await ForeignKeysExistAsync(dto.DepartmentID, dto.FunctionID, dto.IndustryID))
            return null;

        var lesson = new LessonEntity
        {
            ProjectName = dto.ProjectName,
            TitleName = dto.TitleName,
            Description = dto.Description,
            ValueProposition = dto.ValueProposition,
            TargetAudience = dto.TargetAudience,
            PersonToContact = dto.PersonToContact,
            ImageURL = dto.ImageURL,
            DepartmentID = dto.DepartmentID,
            FunctionID = dto.FunctionID,
            IndustryID = dto.IndustryID,
            ModifiedDate = DateTime.UtcNow
        };

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(lesson.LessonID);
    }

    // null = invalid FK, false = not found, true = success
    public async Task<bool?> UpdateAsync(int id, UpdateLessonDto dto)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null) return false;

        if (!await ForeignKeysExistAsync(dto.DepartmentID, dto.FunctionID, dto.IndustryID))
            return null;

        lesson.ProjectName = dto.ProjectName;
        lesson.TitleName = dto.TitleName;
        lesson.Description = dto.Description;
        lesson.ValueProposition = dto.ValueProposition;
        lesson.TargetAudience = dto.TargetAudience;
        lesson.PersonToContact = dto.PersonToContact;
        lesson.ImageURL = dto.ImageURL;
        lesson.DepartmentID = dto.DepartmentID;
        lesson.FunctionID = dto.FunctionID;
        lesson.IndustryID = dto.IndustryID;
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

    private static LessonDto ToDto(LessonEntity l) => new()
    {
        LessonID = l.LessonID,
        ProjectName = l.ProjectName,
        TitleName = l.TitleName,
        Description = l.Description,
        ValueProposition = l.ValueProposition,
        TargetAudience = l.TargetAudience,
        PersonToContact = l.PersonToContact,
        ImageURL = l.ImageURL,
        FunctionID = l.FunctionID,
        DepartmentID = l.DepartmentID,
        IndustryID = l.IndustryID,
        FunctionName = l.Function?.FunctionName,
        DepartmentName = l.Department?.DepartmentName,
        IndustryName = l.Industry?.IndustryName,
        ModifiedDate = l.ModifiedDate
    };
}