namespace Domain.Entities;

public class Department
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public ICollection<DepartmentFunction> DepartmentFunctions { get; set; } = new List<DepartmentFunction>();
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
