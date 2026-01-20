namespace GraphQL.DTOs.UserDto
{
    public record CreateUserDto(
     string Username,
     string Email,
     string FirstName,
     string LastName,
     string? Address,
     string? ContactNumber,
     string CreatedBy
    );
}
