using CA.BLL.Interfaces;
using CA.DAL.Constants;
using CA.DAL.Entities;
using CA.DAL.Enums;
using CA.DAL.Exceptions;
using CA.DAL.Interfaces;
using CA.DAL.Persistence;
using CA.BLL.DTOs;

namespace CA.BLL.Services;

public class LitterService(
    ILitterRepository litterRepository,
    IBenefitRepository benefitRepository,
    INotificationService notificationService,
    AppDbContext context) : ILitterService
{
    private readonly ILitterRepository _litterRepository = litterRepository;
    private readonly IBenefitRepository _benefitRepository = benefitRepository;
    private readonly INotificationService _notificationService = notificationService;
    private readonly AppDbContext _context = context;

    // TODO: Винести координацію SaveChanges/транзакції в окремий IUnitOfWork,
    // якщо кількість репозиторіїв та точок збереження зросте — зараз це не виправдано для 2-3 сутностей.

    public async Task<PublishResponse> PublishAsync(Guid litterId, Guid breederId)
    {
        var litter = await _litterRepository.GetByIdAsync(litterId)
            ?? throw new NotFoundException($"Litter {litterId} not found.");

        if (litter.BreederId != breederId)
            throw new ForbiddenException("You can only publish your own litters.");

        if (litter.Status != LitterStatus.Approved)
            throw new DomainException("Litter must be approved to publish.");

        var benefit = await _benefitRepository.GetByBreederIdAsync(breederId)
            ?? throw new NotFoundException($"Breeder benefit {breederId} not found.");

        if (benefit.UsedCount >= benefit.FreeLimit)
        {
            _context.Logs.Add(new AuditLog(litter.Id, AuditActions.PublishFailedLimitExceeded));

            await _context.SaveChangesAsync();

            throw new DomainException("You have run out of free publications.");
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {

            benefit.EnlargeUsedCount(1);

            litter.ChangeStatus(LitterStatus.Published);

            _context.Logs.Add(new AuditLog(litter.Id, AuditActions.PublishedForFree));

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        await _notificationService.NotifyLitterPublishedAsync(breederId, litter.Id);

        return new PublishResponse(litter.Id, litter.Status.ToString());
    }

    public async Task<PagedLitterResponse> GetLittersAsync(Guid breederId, GetLittersRequest request)
    {
        var (items, totalCount) = await _litterRepository.GetByBreederAsync(
            breederId, request.Status, request.PageSize, request.PageNumber);

        var mapped = items.Select(l => new LitterResponse(
            l.Id, l.BreederId, l.Status.ToString(), l.CreatedAt));

        return new PagedLitterResponse(mapped, totalCount, request.PageNumber, request.PageSize);
    }
}