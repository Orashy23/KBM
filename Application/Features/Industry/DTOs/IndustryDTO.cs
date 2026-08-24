namespace Application.Features.Industry.DTOs;

public class IndustryDto
{
    public int IndustryID { get; set; }
    public string IndustryName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}

public class CreateIndustryDto
{
    public string IndustryName { get; set; } = string.Empty;
}

public class UpdateIndustryDto
{
    public string IndustryName { get; set; } = string.Empty;
}