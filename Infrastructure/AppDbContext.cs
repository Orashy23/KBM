using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Function> Functions => Set<Function>();
    public DbSet<DepartmentFunction> DepartmentFunctions => Set<DepartmentFunction>();
    public DbSet<Industry> Industries => Set<Industry>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new FunctionConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentFunctionConfiguration());
        modelBuilder.ApplyConfiguration(new IndustryConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
    }
}