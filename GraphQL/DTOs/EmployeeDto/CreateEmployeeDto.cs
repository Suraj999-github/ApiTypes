namespace GraphQL.DTOs.EmployeeDto
{
    public record CreateEmployeeDto(
        string EmployeeCode,
        string FirstName,
        string LastName,
        string Email,
        string? Department,
        string? Position,
        decimal? Salary,
        DateTime? HireDate,
        string? ContactNumber,
        string? Address,
        int? ManagerId,
        string CreatedBy
    );
}
