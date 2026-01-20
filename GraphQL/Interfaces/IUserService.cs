using GraphQL.DTOs.Pagination;
using GraphQL.DTOs.UserDto;

namespace GraphQL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<PaginatedResult<UserDto>> GetUsersAsync(PaginationInput pagination);
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<UserDto?> UpdateUserAsync(UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
