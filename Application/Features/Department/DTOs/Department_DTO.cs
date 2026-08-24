namespace Application.Features.Department.DTOs;

public class DepartmentDto
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

public class CreateDepartmentDto
{
    public string DepartmentName { get; set; } = string.Empty;
}

public class UpdateDepartmentDto
{
    public string DepartmentName { get; set; } = string.Empty;
}