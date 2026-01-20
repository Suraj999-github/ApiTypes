using GraphQL.DTOs.Pagination;
using GraphQL.DTOs.UserDto;
using GraphQL.Interfaces;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? MapToDto(user) : null;
        }

        public async Task<PaginatedResult<UserDto>> GetUsersAsync(PaginationInput pagination)
        {
            var query = _userRepository.GetQueryable();
            var totalCount = await query.CountAsync();

            var users = await query
                .OrderByDescending(u => u.CreatedAt)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            var userDtos = users.Select(MapToDto);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new PaginatedResult<UserDto>(
                userDtos,
                totalCount,
                pagination.Page,
                pagination.PageSize,
                totalPages
            );
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                FullName = $"{dto.FirstName} {dto.LastName}",
                Address = dto.Address,
                ContactNumber = dto.ContactNumber,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy
            };

            var createdUser = await _userRepository.AddAsync(user);
            return MapToDto(createdUser);
        }

        public async Task<UserDto?> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.Id);
            if (user == null) return null;

            if (!string.IsNullOrEmpty(dto.Username)) user.Username = dto.Username;
            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.FirstName)) user.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) user.LastName = dto.LastName;
            if (dto.Address != null) user.Address = dto.Address;
            if (dto.ContactNumber != null) user.ContactNumber = dto.ContactNumber;

            user.FullName = $"{user.FirstName} {user.LastName}";
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = dto.UpdatedBy;

            var updatedUser = await _userRepository.UpdateAsync(user);
            return MapToDto(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto(
                user.Id,
                user.Username,
                user.Email,
                user.FirstName,
                user.LastName,
                user.FullName,
                user.Address,
                user.ContactNumber,
                user.CreatedAt,
                user.UpdatedAt,
                user.CreatedBy,
                user.UpdatedBy
            );
        }
    }
}
