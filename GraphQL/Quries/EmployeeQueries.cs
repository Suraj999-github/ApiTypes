using GraphQL.Common;
using GraphQL.DTOs.EmployeeDto;
using GraphQL.DTOs.Pagination;
using GraphQL.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GraphQL.Quries
{
    [ExtendObjectType(typeof(Query))]
    public class EmployeeQueries
    {
        [GraphQLName("employeeById")]
        public async Task<ApiResponse<EmployeeDto>> GetEmployeeById(
            [Service] IEmployeeService employeeService,
            int id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return ApiResponse<EmployeeDto>.NOT_FOUND(
                        description: $"Employee with ID {id} not found"
                    );
                }

                return ApiResponse<EmployeeDto>.SUCCESS(
                    data: employee,
                    entityId: employee.Id.ToString(),
                    description: "Employee retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<EmployeeDto>.ERROR(
                    description: $"Error retrieving employee: {ex.Message}"
                );
            }
        }

        [GraphQLName("employees")]
        public async Task<ApiResponse<PaginatedResult<EmployeeDto>>> GetEmployees(
            [Service] IEmployeeService employeeService,
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                var pagination = new PaginationInput(page, pageSize);
                var employees = await employeeService.GetEmployeesAsync(pagination);

                return ApiResponse<PaginatedResult<EmployeeDto>>.SUCCESS(
                    data: employees,
                    description: $"Retrieved {employees.Items.Count()} employees successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<PaginatedResult<EmployeeDto>>.ERROR(
                    description: $"Error retrieving employees: {ex.Message}"
                );
            }
        }

        [GraphQLName("employeesByDepartment")]
        public async Task<ApiResponse<PaginatedResult<EmployeeDto>>> GetEmployeesByDepartment(
            [Service] IEmployeeService employeeService,
            string department,
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                var pagination = new PaginationInput(page, pageSize);
                var employees = await employeeService.GetEmployeesByDepartmentAsync(department, pagination);

                return ApiResponse<PaginatedResult<EmployeeDto>>.SUCCESS(
                    data: employees,
                    description: $"Retrieved {employees.Items.Count()} employees from {department} department"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<PaginatedResult<EmployeeDto>>.ERROR(
                    description: $"Error retrieving employees by department: {ex.Message}"
                );
            }
        }
    }
}
