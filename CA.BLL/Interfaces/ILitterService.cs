using CA.BLL.DTOs;

namespace CA.BLL.Interfaces;

public interface ILitterService
{
    Task<PublishResponse> PublishAsync(Guid litterId, Guid breederId);

    Task<PagedLitterResponse> GetLittersAsync(Guid breederId, GetLittersRequest request);
}