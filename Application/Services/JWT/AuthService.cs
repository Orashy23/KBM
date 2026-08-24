using Application.Services.JWT.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.JWT;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(AppDbContext context, TokenService tokenService, ILogger<AuthService> logger)
    {
        _context = context;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        var usernameOrEmailTaken = await _context.Users
            .AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email);

        if (usernameOrEmailTaken)
        {
            _logger.LogWarning("Registration rejected — username or email already in use for {Username}", dto.Username);
            throw new InvalidOperationException("Username or email is already in use.");
        }

        var user = new User
        {
            FullName = dto.FullName,
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role,
            CreatedDate = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation("New user registered: {Username} (ID {UserId}) with role {Role}",
            user.Username, user.UserID, user.Role);

        var token = _tokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            // Never log the submitted password.
            _logger.LogWarning("Failed login attempt for username {Username}", dto.Username);
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        _logger.LogInformation("User {Username} (ID {UserId}) logged in", user.Username, user.UserID);

        var token = _tokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        };
    }
}
