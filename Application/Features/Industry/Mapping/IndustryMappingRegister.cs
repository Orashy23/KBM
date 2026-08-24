using Application.Features.Industry.DTOs;
using Mapster;
using IndustryEntity = Domain.Entities.Industry;

namespace Application.Features.Industry.Mapping;

public class IndustryMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IndustryEntity, IndustryDto>();

        config.NewConfig<CreateIndustryDto, IndustryEntity>()
              .Ignore(dest => dest.IndustryID)
              .Ignore(dest => dest.Lessons);

        config.NewConfig<UpdateIndustryDto, IndustryEntity>()
              .Ignore(dest => dest.IndustryID)
              .Ignore(dest => dest.CreatedDate)
              .Ignore(dest => dest.ModifiedDate)
              .Ignore(dest => dest.Lessons);
    }
}
