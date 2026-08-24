using Application.Features.Function.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using FunctionEntity = Domain.Entities.Function;

namespace Application.Features.Function.Services;

public class FunctionService
{
    private readonly AppDbContext _context;
    public FunctionService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<FunctionDto>> GetAllAsync() =>
        await _context.Functions
            .Select(f => new FunctionDto
            {
                FunctionID = f.FunctionID,
                FunctionName = f.FunctionName,
                CreatedDate = f.CreatedDate,
                UpdatedDate = f.UpdatedDate
            })
            .ToListAsync();

    public async Task<FunctionDto?> GetByIdAsync(int id)
    {
        var function = await _context.Functions.FindAsync(id);
        if (function == null) return null;

        return new FunctionDto
        {
            FunctionID = function.FunctionID,
            FunctionName = function.FunctionName,
            CreatedDate = function.CreatedDate,
            UpdatedDate = function.UpdatedDate
        };
    }

    public async Task<FunctionDto> CreateAsync(CreateFunctionDto dto)
    {
        var function = new FunctionEntity
        {
            FunctionName = dto.FunctionName,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        _context.Functions.Add(function);
        await _context.SaveChangesAsync();

        return new FunctionDto
        {
            FunctionID = function.FunctionID,
            FunctionName = function.FunctionName,
            CreatedDate = function.CreatedDate,
            UpdatedDate = function.UpdatedDate
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateFunctionDto dto)
    {
        var function = await _context.Functions.FindAsync(id);
        if (function == null) return false;

        function.FunctionName = dto.FunctionName;
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