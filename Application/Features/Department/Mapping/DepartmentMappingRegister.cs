using Application.Features.Department.DTOs;
using Mapster;
using DepartmentEntity = Domain.Entities.Department;

namespace Application.Features.Department.Mapping;

public class DepartmentMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Entity -> DTO: every property matches by name.
        config.NewConfig<DepartmentEntity, DepartmentDto>();

        // Create: timestamps come from the entity's own initializers (UtcNow).
        config.NewConfig<CreateDepartmentDto, DepartmentEntity>()
              .Ignore(dest => dest.DepartmentID)
              .Ignore(dest => dest.DepartmentFunctions)
              .Ignore(dest => dest.Lessons);

        // Update: applied onto a tracked entity, so the key and timestamps are left alone.
        config.NewConfig<UpdateDepartmentDto, DepartmentEntity>()
              .Ignore(dest => dest.DepartmentID)
              .Ignore(dest => dest.CreatedDate)
              .Ignore(dest => dest.UpdatedDate)
              .Ignore(dest => dest.DepartmentFunctions)
              .Ignore(dest => dest.Lessons);
    }
}
