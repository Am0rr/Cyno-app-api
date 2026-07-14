using CA.DAL.Entities;
using CA.DAL.Enums;

namespace CA.DAL.Interfaces;

public interface ILitterRepository
{
    Task<Litter?> GetByIdAsync(Guid id);
    Task<(List<Litter> items, int totalCount)> GetByBreederAsync(
        Guid breederId, LitterStatus? status, int pageSize, int pageNumber);
}