namespace Domain.Entities;

public class Lesson
{
    public int LessonID { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string TitleName { get; set; } = string.Empty;

    // Optional detail fields — nullable in the database, but the service layer
    // writes string.Empty rather than null when the client omits them.
    public string? Description { get; set; }
    public string? ValueProposition { get; set; }
    public string? TargetAudience { get; set; }
    public string? PersonToContact { get; set; }
    public string? ImageURL { get; set; }

    public int FunctionID { get; set; }
    public int DepartmentID { get; set; }
    public int IndustryID { get; set; }

    public Function? Function { get; set; }
    public Department? Department { get; set; }
    public Industry? Industry { get; set; }

    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
}
