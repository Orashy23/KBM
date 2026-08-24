using Application.Features.Function.DTOs;
using Mapster;
using FunctionEntity = Domain.Entities.Function;

namespace Application.Features.Function.Mapping;

public class FunctionMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FunctionEntity, FunctionDto>();

        config.NewConfig<CreateFunctionDto, FunctionEntity>()
              .Ignore(dest => dest.FunctionID)
              .Ignore(dest => dest.DepartmentFunctions)
              .Ignore(dest => dest.Lessons);

        config.NewConfig<UpdateFunctionDto, FunctionEntity>()
              .Ignore(dest => dest.FunctionID)
              .Ignore(dest => dest.CreatedDate)
              .Ignore(dest => dest.UpdatedDate)
              .Ignore(dest => dest.DepartmentFunctions)
              .Ignore(dest => dest.Lessons);
    }
}
