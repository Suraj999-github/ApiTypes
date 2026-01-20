using GraphQL.DTOs.EmployeeDto;
using GraphQL.DTOs.Pagination;

namespace GraphQL.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<PaginatedResult<EmployeeDto>> GetEmployeesAsync(PaginationInput pagination);
        Task<PaginatedResult<EmployeeDto>> GetEmployeesByDepartmentAsync(string department, PaginationInput pagination);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> DeactivateEmployeeAsync(int id, string updatedBy);
    }
}
