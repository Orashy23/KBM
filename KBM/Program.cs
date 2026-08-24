using Application.Features.Department.Services;
using Application.Features.DepartmentFunction.Services;
using Application.Features.Function.Services;
using Application.Features.Industry.Services;
using Application.Features.Lesson.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<FunctionService>();
builder.Services.AddScoped<DepartmentFunctionService>();
builder.Services.AddScoped<IndustryService>();
builder.Services.AddScoped<LessonService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();