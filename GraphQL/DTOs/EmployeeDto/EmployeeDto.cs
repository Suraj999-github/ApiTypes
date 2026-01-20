namespace GraphQL.DTOs.EmployeeDto
{
    public record EmployeeDto(
        int Id,
        string EmployeeCode,
        string FirstName,
        string LastName,
        string FullName,
        string Email,
        string? Department,
        string? Position,
        decimal? Salary,
        DateTime? HireDate,
        string? ContactNumber,
        string? Address,
        bool IsActive,
        int? ManagerId,
        string? ManagerName,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        string CreatedBy,
        string? UpdatedBy
    );
}
