using Application.Features.Industry.DTOs;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using IndustryEntity = Domain.Entities.Industry;

namespace Application.Features.Industry.Services;

public class IndustryService
{
    private readonly AppDbContext _context;
    public IndustryService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<IndustryDto>> GetAllAsync() =>
        await _context.Industries
            .ProjectToType<IndustryDto>()
            .ToListAsync();

    public async Task<IndustryDto?> GetByIdAsync(int id) =>
        await _context.Industries
            .Where(i => i.IndustryID == id)
            .ProjectToType<IndustryDto>()
            .FirstOrDefaultAsync();

    public async Task<IndustryDto> CreateAsync(CreateIndustryDto dto)
    {
        var industry = dto.Adapt<IndustryEntity>();

        _context.Industries.Add(industry);
        await _context.SaveChangesAsync();

        return industry.Adapt<IndustryDto>();
    }

    public async Task<bool> UpdateAsync(int id, UpdateIndustryDto dto)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null) return false;

        dto.Adapt(industry);
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
