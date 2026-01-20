using GraphQL.Common;
using GraphQL.DTOs.UserDto;
using GraphQL.Interfaces;

namespace GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutations
    {
        [GraphQLName("createUser")]
        public async Task<ApiResponse<UserDto>> CreateUser(
            [Service] IUserService userService,
            CreateUserDto input)
        {
            try
            {
                var user = await userService.CreateUserAsync(input);

                return ApiResponse<UserDto>.SUCCESS(
                    data: user,
                    entityId: user.Id.ToString(),
                    description: "User created successfully"
                );
            }
            catch (Exception ex)
            {
                // Check for specific exceptions
                if (ex.Message.Contains("duplicate") || ex.Message.Contains("already exists"))
                {
                    return ApiResponse<UserDto>.CONFLICT(
                        description: $"User with username '{input.Username}' or email '{input.Email}' already exists"
                    );
                }

                return ApiResponse<UserDto>.ERROR(
                    description: $"Error creating user: {ex.Message}"
                );
            }
        }

        [GraphQLName("updateUser")]
        public async Task<ApiResponse<UserDto>> UpdateUser(
            [Service] IUserService userService,
            UpdateUserDto input)
        {
            try
            {
                var user = await userService.UpdateUserAsync(input);

                if (user == null)
                {
                    return ApiResponse<UserDto>.NOT_FOUND(
                        description: $"User with ID {input.Id} not found"
                    );
                }

                return ApiResponse<UserDto>.SUCCESS(
                    data: user,
                    entityId: user.Id.ToString(),
                    description: "User updated successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.ERROR(
                    description: $"Error updating user: {ex.Message}"
                );
            }
        }

        [GraphQLName("deleteUser")]
        public async Task<ApiResponse<bool>> DeleteUser(
            [Service] IUserService userService,
            int id)
        {
            try
            {
                var deleted = await userService.DeleteUserAsync(id);

                if (!deleted)
                {
                    return ApiResponse<bool>.NOT_FOUND(
                        data: false,
                        description: $"User with ID {id} not found"
                    );
                }

                return ApiResponse<bool>.SUCCESS(
                    data: true,
                    entityId: id.ToString(),
                    description: "User deleted successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ERROR(
                    data: false,
                    description: $"Error deleting user: {ex.Message}"
                );
            }
        }
    }
}
