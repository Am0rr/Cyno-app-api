namespace CA.BLL.DTOs;

public record LitterResponse(
    Guid Id,
    Guid BreederId,
    string Status,
    DateTime CreatedAt
);