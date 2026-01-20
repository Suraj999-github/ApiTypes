using GraphQL.Common;
using GraphQL.DTOs.Pagination;
using GraphQL.DTOs.UserDto;
using GraphQL.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GraphQL.Quries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQueries
    {
        [GraphQLName("userById")]
        public async Task<ApiResponse<UserDto>> GetUserById(
            [Service] IUserService userService,
            int id)
        {
            try
            {
                var user = await userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return ApiResponse<UserDto>.NOT_FOUND(
                        description: $"User with ID {id} not found"
                    );
                }

                return ApiResponse<UserDto>.SUCCESS(
                    data: user,
                    entityId: user.Id.ToString(),
                    description: "User retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.ERROR(
                    description: $"Error retrieving user: {ex.Message}"
                );
            }
        }

        [GraphQLName("users")]
        public async Task<ApiResponse<PaginatedResult<UserDto>>> GetUsers(
            [Service] IUserService userService,
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                var pagination = new PaginationInput(page, pageSize);
                var users = await userService.GetUsersAsync(pagination);

                return ApiResponse<PaginatedResult<UserDto>>.SUCCESS(
                    data: users,
                    description: $"Retrieved {users.Items.Count()} users successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<PaginatedResult<UserDto>>.ERROR(
                    description: $"Error retrieving users: {ex.Message}"
                );
            }
        }
    }
}
