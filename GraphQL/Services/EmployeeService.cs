using GraphQL.DTOs.EmployeeDto;
using GraphQL.DTOs.Pagination;
using GraphQL.Interfaces;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<User> _userRepository;

        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<User> userRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetQueryable()
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee != null ? MapToDto(employee) : null;
        }

        public async Task<PaginatedResult<EmployeeDto>> GetEmployeesAsync(PaginationInput pagination)
        {
            var query = _employeeRepository.GetQueryable().Include(e => e.Manager);
            var totalCount = await query.CountAsync();

            var employees = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            var employeeDtos = employees.Select(MapToDto);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PaginatedResult<EmployeeDto>(
                employeeDtos,
                totalCount,
                pagination.Page,
                pagination.PageSize,
                totalPages
            );
        }

        public async Task<PaginatedResult<EmployeeDto>> GetEmployeesByDepartmentAsync(
            string department,
            PaginationInput pagination)
        {
            var query = _employeeRepository.GetQueryable()
                .Include(e => e.Manager)
                .Where(e => e.Department == department);

            var totalCount = await query.CountAsync();

            var employees = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            var employeeDtos = employees.Select(MapToDto);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PaginatedResult<EmployeeDto>(
                employeeDtos,
                totalCount,
                pagination.Page,
                pagination.PageSize,
                totalPages
            );
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                EmployeeCode = dto.EmployeeCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                FullName = $"{dto.FirstName} {dto.LastName}",
                Email = dto.Email,
                Department = dto.Department,
                Position = dto.Position,
                Salary = dto.Salary,
                HireDate = dto.HireDate,
                ContactNumber = dto.ContactNumber,
                Address = dto.Address,
                ManagerId = dto.ManagerId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy
            };

            var createdEmployee = await _employeeRepository.AddAsync(employee);

            var employeeWithManager = await _employeeRepository.GetQueryable()
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(e => e.Id == createdEmployee.Id);

            return MapToDto(employeeWithManager!);
        }

        public async Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetQueryable()
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(e => e.Id == dto.Id);

            if (employee == null) return null;

            if (!string.IsNullOrEmpty(dto.EmployeeCode)) employee.EmployeeCode = dto.EmployeeCode;
            if (!string.IsNullOrEmpty(dto.FirstName)) employee.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) employee.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(dto.Email)) employee.Email = dto.Email;
            if (dto.Department != null) employee.Department = dto.Department;
            if (dto.Position != null) employee.Position = dto.Position;
            if (dto.Salary.HasValue) employee.Salary = dto.Salary;
            if (dto.HireDate.HasValue) employee.HireDate = dto.HireDate;
            if (dto.ContactNumber != null) employee.ContactNumber = dto.ContactNumber;
            if (dto.Address != null) employee.Address = dto.Address;
            if (dto.IsActive.HasValue) employee.IsActive = dto.IsActive.Value;
            if (dto.ManagerId.HasValue) employee.ManagerId = dto.ManagerId;

            employee.FullName = $"{employee.FirstName} {employee.LastName}";
            employee.UpdatedAt = DateTime.UtcNow;
            employee.UpdatedBy = dto.UpdatedBy;

            var updatedEmployee = await _employeeRepository.UpdateAsync(employee);
            return MapToDto(updatedEmployee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<bool> DeactivateEmployeeAsync(int id, string updatedBy)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return false;

            employee.IsActive = false;
            employee.UpdatedAt = DateTime.UtcNow;
            employee.UpdatedBy = updatedBy;

            await _employeeRepository.UpdateAsync(employee);
            return true;
        }

        private static EmployeeDto MapToDto(Employee employee)
        {
            return new EmployeeDto(
                employee.Id,
                employee.EmployeeCode,
                employee.FirstName,
                employee.LastName,
                employee.FullName,
                employee.Email,
                employee.Department,
                employee.Position,
                employee.Salary,
                employee.HireDate,
                employee.ContactNumber,
                employee.Address,
                employee.IsActive,
                employee.ManagerId,
                employee.Manager?.FullName,
                employee.CreatedAt,
                employee.UpdatedAt,
                employee.CreatedBy,
                employee.UpdatedBy
            );
        }
    }
}
