using Application.Features.Lesson.DTOs;
using Mapster;
using LessonEntity = Domain.Entities.Lesson;

namespace Application.Features.Lesson.Mapping;

public class LessonMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Entity -> DTO, flattening the related names. Written as explicit maps so
        // ProjectToType can translate them into LEFT JOINs in a single SQL query.
        config.NewConfig<LessonEntity, LessonDto>()
              .Map(dest => dest.FunctionName, src => src.Function!.FunctionName)
              .Map(dest => dest.DepartmentName, src => src.Department!.DepartmentName)
              .Map(dest => dest.IndustryName, src => src.Industry!.IndustryName);

        config.NewConfig<CreateLessonDto, LessonEntity>()
              .Ignore(dest => dest.LessonID)
              .Ignore(dest => dest.ModifiedDate)
              .Ignore(dest => dest.Function)
              .Ignore(dest => dest.Department)
              .Ignore(dest => dest.Industry)
              .Map(dest => dest.Description, src => src.Description ?? string.Empty)
              .Map(dest => dest.ValueProposition, src => src.ValueProposition ?? string.Empty)
              .Map(dest => dest.TargetAudience, src => src.TargetAudience ?? string.Empty)
              .Map(dest => dest.PersonToContact, src => src.PersonToContact ?? string.Empty)
              .Map(dest => dest.ImageURL, src => src.ImageURL ?? string.Empty);

        config.NewConfig<UpdateLessonDto, LessonEntity>()
              .Ignore(dest => dest.LessonID)
              .Ignore(dest => dest.ModifiedDate)
              .Ignore(dest => dest.Function)
              .Ignore(dest => dest.Department)
              .Ignore(dest => dest.Industry)
              .Map(dest => dest.Description, src => src.Description ?? string.Empty)
              .Map(dest => dest.ValueProposition, src => src.ValueProposition ?? string.Empty)
              .Map(dest => dest.TargetAudience, src => src.TargetAudience ?? string.Empty)
              .Map(dest => dest.PersonToContact, src => src.PersonToContact ?? string.Empty)
              .Map(dest => dest.ImageURL, src => src.ImageURL ?? string.Empty);
    }
}
