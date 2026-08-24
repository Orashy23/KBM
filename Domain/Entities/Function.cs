namespace Domain.Entities;

public class Function
{
    public int FunctionID { get; set; }
    public string FunctionName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public ICollection<DepartmentFunction> DepartmentFunctions { get; set; } = new List<DepartmentFunction>();
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
