namespace Application.Features.DepartmentFunction.DTOs;

public class DepartmentFunctionDto
{
    public int DepartmentID { get; set; }
    public int FunctionID { get; set; }
    public string? DepartmentName { get; set; }
    public string? FunctionName { get; set; }
}

public class CreateDepartmentFunctionDto
{
    public int DepartmentID { get; set; }
    public int FunctionID { get; set; }
}