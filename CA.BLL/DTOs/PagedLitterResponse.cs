namespace CA.BLL.DTOs;

public record PagedLitterResponse(
    IEnumerable<LitterResponse> Items,
    int TotalCount,
    int PageNumber,
    int PageSize
);