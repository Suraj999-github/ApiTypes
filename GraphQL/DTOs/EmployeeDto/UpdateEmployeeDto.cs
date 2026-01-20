namespace GraphQL.DTOs.EmployeeDto
{
    public record UpdateEmployeeDto(
        int Id,
        string? EmployeeCode,
        string? FirstName,
        string? LastName,
        string? Email,
        string? Department,
        string? Position,
        decimal? Salary,
        DateTime? HireDate,
        string? ContactNumber,
        string? Address,
        bool? IsActive,
        int? ManagerId,
        string UpdatedBy
    );
}
