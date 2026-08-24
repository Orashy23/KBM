namespace Domain.Entities;

public class DepartmentFunction
{
    public int FunctionID { get; set; }
    public int DepartmentID { get; set; }

    public Department? Department { get; set; }
    public Function? Function { get; set; }
}
