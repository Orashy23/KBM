namespace Application.Features.Function.DTOs;

public class FunctionDto
{
    public int FunctionID { get; set; }
    public string FunctionName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

public class CreateFunctionDto
{
    public string FunctionName { get; set; } = string.Empty;
}

public class UpdateFunctionDto
{
    public string FunctionName { get; set; } = string.Empty;
}