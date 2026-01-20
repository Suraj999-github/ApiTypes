namespace GraphQL.DTOs.UserDto
{
    public record UserDto(
         int Id,
         string Username,
         string Email,
         string FirstName,
         string LastName,
         string FullName,
         string? Address,
         string? ContactNumber,
         DateTime CreatedAt,
         DateTime? UpdatedAt,
         string CreatedBy,
         string? UpdatedBy
     );
}
