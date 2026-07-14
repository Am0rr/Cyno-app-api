using CA.DAL.Enums;

namespace CA.BLL.DTOs;

public record GetLittersRequest(
    LitterStatus? Status,
    int PageNumber = 1,
    int PageSize = 10
);

