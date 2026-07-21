using CA.DAL.Entities;
using CA.DAL.Enums;
using CA.DAL.Interfaces;
using CA.DAL.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CA.DAL.Repositories;

public class LitterRepository(AppDbContext context) : ILitterRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Litter?> GetByIdAsync(Guid id)
    {
        return await _context.Litters.FindAsync(id);
    }

    public async Task<(List<Litter> items, int totalCount)> GetByBreederAsync(
        Guid breederId, LitterStatus? status, int pageSize, int pageNumber)
    {
        var query = _context.Litters.Where(l => l.BreederId == breederId);

        if (status.HasValue)
            query = query.Where(l => l.Status == status);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(l => l.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}