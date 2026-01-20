namespace GraphQL.DTOs.Pagination
{
    public record PaginationInput(
       int Page = 1,
       int PageSize = 10
   );
    public record PaginatedResult<T>(
    IEnumerable<T> Items,
    int TotalCount,
    int Page,
    int PageSize,
    int TotalPages
) where T : class;
}
