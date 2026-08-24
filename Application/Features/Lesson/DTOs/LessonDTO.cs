namespace Application.Features.Lesson.DTOs;

public class LessonDto
{
    public int LessonID { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string TitleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ValueProposition { get; set; }
    public string? TargetAudience { get; set; }
    public string? PersonToContact { get; set; }
    public string? ImageURL { get; set; }

    public int FunctionID { get; set; }
    public int DepartmentID { get; set; }
    public int IndustryID { get; set; }

    public string? FunctionName { get; set; }
    public string? DepartmentName { get; set; }
    public string? IndustryName { get; set; }

    public DateTime ModifiedDate { get; set; }
}

public class CreateLessonDto
{
    public string ProjectName { get; set; } = string.Empty;
    public string TitleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ValueProposition { get; set; }
    public string? TargetAudience { get; set; }
    public string? PersonToContact { get; set; }
    public string? ImageURL { get; set; }

    public int FunctionID { get; set; }
    public int DepartmentID { get; set; }
    public int IndustryID { get; set; }
}

public class UpdateLessonDto : CreateLessonDto
{
}