using Application.Features.Industry.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using IndustryEntity = Domain.Entities.Industry;

namespace Application.Features.Industry.Services;

public class IndustryService
{
    private readonly AppDbContext _context;
    public IndustryService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<IndustryDto>> GetAllAsync() =>
        await _context.Industries
            .Select(i => new IndustryDto
            {
                IndustryID = i.IndustryID,
                IndustryName = i.IndustryName,
                CreatedDate = i.CreatedDate,
                ModifiedDate = i.ModifiedDate
            })
            .ToListAsync();

    public async Task<IndustryDto?> GetByIdAsync(int id)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null) return null;

        return new IndustryDto
        {
            IndustryID = industry.IndustryID,
            IndustryName = industry.IndustryName,
            CreatedDate = industry.CreatedDate,
            ModifiedDate = industry.ModifiedDate
        };
    }

    public async Task<IndustryDto> CreateAsync(CreateIndustryDto dto)
    {
        var industry = new IndustryEntity
        {
            IndustryName = dto.IndustryName,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        _context.Industries.Add(industry);
        await _context.SaveChangesAsync();

        return new IndustryDto
        {
            IndustryID = industry.IndustryID,
            IndustryName = industry.IndustryName,
            CreatedDate = industry.CreatedDate,
            ModifiedDate = industry.ModifiedDate
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateIndustryDto dto)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null) return false;

        industry.IndustryName = dto.IndustryName;
        industry.ModifiedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null) return false;

        _context.Industries.Remove(industry);
        await _context.SaveChangesAsync();
        return true;
    }
}