using GraphQL.Common;
using GraphQL.DTOs.EmployeeDto;
using GraphQL.Interfaces;

namespace GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class EmployeeMutations
    {
        [GraphQLName("createEmployee")]
        public async Task<ApiResponse<EmployeeDto>> CreateEmployee(
            [Service] IEmployeeService employeeService,
            CreateEmployeeDto input)
        {
            try
            {
                var employee = await employeeService.CreateEmployeeAsync(input);

                return ApiResponse<EmployeeDto>.SUCCESS(
                    data: employee,
                    entityId: employee.Id.ToString(),
                    description: "Employee created successfully"
                );
            }
            catch (Exception ex)
            {
                // Check for specific exceptions
                if (ex.Message.Contains("duplicate") || ex.Message.Contains("already exists"))
                {
                    return ApiResponse<EmployeeDto>.CONFLICT(
                        description: $"Employee with code '{input.EmployeeCode}' or email '{input.Email}' already exists"
                    );
                }

                return ApiResponse<EmployeeDto>.ERROR(
                    description: $"Error creating employee: {ex.Message}"
                );
            }
        }

        [GraphQLName("updateEmployee")]
        public async Task<ApiResponse<EmployeeDto>> UpdateEmployee(
            [Service] IEmployeeService employeeService,
            UpdateEmployeeDto input)
        {
            try
            {
                var employee = await employeeService.UpdateEmployeeAsync(input);

                if (employee == null)
                {
                    return ApiResponse<EmployeeDto>.NOT_FOUND(
                        description: $"Employee with ID {input.Id} not found"
                    );
                }

                return ApiResponse<EmployeeDto>.SUCCESS(
                    data: employee,
                    entityId: employee.Id.ToString(),
                    description: "Employee updated successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<EmployeeDto>.ERROR(
                    description: $"Error updating employee: {ex.Message}"
                );
            }
        }

        [GraphQLName("deleteEmployee")]
        public async Task<ApiResponse<bool>> DeleteEmployee(
            [Service] IEmployeeService employeeService,
            int id)
        {
            try
            {
                var deleted = await employeeService.DeleteEmployeeAsync(id);

                if (!deleted)
                {
                    return ApiResponse<bool>.NOT_FOUND(
                        data: false,
                        description: $"Employee with ID {id} not found"
                    );
                }

                return ApiResponse<bool>.SUCCESS(
                    data: true,
                    entityId: id.ToString(),
                    description: "Employee deleted successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ERROR(
                    data: false,
                    description: $"Error deleting employee: {ex.Message}"
                );
            }
        }

        [GraphQLName("deactivateEmployee")]
        public async Task<ApiResponse<bool>> DeactivateEmployee(
            [Service] IEmployeeService employeeService,
            int id,
            string updatedBy)
        {
            try
            {
                var deactivated = await employeeService.DeactivateEmployeeAsync(id, updatedBy);

                if (!deactivated)
                {
                    return ApiResponse<bool>.NOT_FOUND(
                        data: false,
                        description: $"Employee with ID {id} not found"
                    );
                }

                return ApiResponse<bool>.SUCCESS(
                    data: true,
                    entityId: id.ToString(),
                    description: "Employee deactivated successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ERROR(
                    data: false,
                    description: $"Error deactivating employee: {ex.Message}"
                );
            }
        }
    }
}
