using Application.Features.Function.DTOs;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using FunctionEntity = Domain.Entities.Function;

namespace Application.Features.Function.Services;

public class FunctionService
{
    private readonly AppDbContext _context;
    public FunctionService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<FunctionDto>> GetAllAsync() =>
        await _context.Functions
            .ProjectToType<FunctionDto>()
            .ToListAsync();

    public async Task<FunctionDto?> GetByIdAsync(int id) =>
        await _context.Functions
            .Where(f => f.FunctionID == id)
            .ProjectToType<FunctionDto>()
            .FirstOrDefaultAsync();

    public async Task<FunctionDto> CreateAsync(CreateFunctionDto dto)
    {
        var function = dto.Adapt<FunctionEntity>();

        _context.Functions.Add(function);
        await _context.SaveChangesAsync();

        return function.Adapt<FunctionDto>();
    }

    public async Task<bool> UpdateAsync(int id, UpdateFunctionDto dto)
    {
        var function = await _context.Functions.FindAsync(id);
        if (function == null) return false;

        dto.Adapt(function);
        function.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var function = await _context.Functions.FindAsync(id);
        if (function == null) return false;

        _context.Functions.Remove(function);
        await _context.SaveChangesAsync();
        return true;
    }
}
