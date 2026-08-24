using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Features.Lesson.DTOs;

public class CreateLessonDto
{
    // Basic text columns
    public string Title { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string? ValueProposition { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? PersonToContact { get; set; }

    // Foreign Keys linking to parent entities
    public int DepartmentId { get; set; }
    public int FunctionId { get; set; }
    public int IndustryId { get; set; }
}

