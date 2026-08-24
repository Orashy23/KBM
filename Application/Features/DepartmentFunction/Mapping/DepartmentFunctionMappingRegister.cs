using Application.Features.DepartmentFunction.DTOs;
using Mapster;
using DepartmentFunctionEntity = Domain.Entities.DepartmentFunction;

namespace Application.Features.DepartmentFunction.Mapping;

public class DepartmentFunctionMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DepartmentFunctionEntity, DepartmentFunctionDto>()
              .Map(dest => dest.DepartmentName, src => src.Department!.DepartmentName)
              .Map(dest => dest.FunctionName, src => src.Function!.FunctionName);

        config.NewConfig<CreateDepartmentFunctionDto, DepartmentFunctionEntity>()
              .Ignore(dest => dest.Department)
              .Ignore(dest => dest.Function);
    }
}
