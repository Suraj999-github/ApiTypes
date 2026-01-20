namespace GraphQL.DTOs.UserDto
{
    public record UpdateUserDto(
         int Id,
         string? Username,
         string? Email,
         string? FirstName,
         string? LastName,
         string? Address,
         string? ContactNumber,
         string UpdatedBy
     );
}
