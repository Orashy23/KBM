using Application.Features.Department.Services;
using Application.Features.DepartmentFunction.Services;
using Application.Features.Function.Services;
using Application.Features.Industry.Services;
using Application.Features.Lesson.Services;
using Application.Services.JWT;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the Application layer: Mapster mapping configuration plus every feature service.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Discover every IRegister in this assembly and build the mapping config once at startup.
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(typeof(DependencyInjection).Assembly);
        services.AddSingleton(mappingConfig);

        // Feature services
        services.AddScoped<DepartmentService>();
        services.AddScoped<FunctionService>();
        services.AddScoped<IndustryService>();
        services.AddScoped<LessonService>();
        services.AddScoped<DepartmentFunctionService>();

        // Authentication
        services.AddScoped<TokenService>();
        services.AddScoped<AuthService>();

        return services;
    }
}
