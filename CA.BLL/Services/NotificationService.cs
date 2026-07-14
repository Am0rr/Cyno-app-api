using CA.BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace CA.BLL.Services;

public class NotificationService(ILogger<NotificationService> logger) : INotificationService
{
    private readonly ILogger<NotificationService> _logger = logger;

    public Task NotifyLitterPublishedAsync(Guid breederId, Guid litterId)
    {
        _logger.LogInformation(
            "[Email] To breeder {BreederId}: litter {LitterId} published.",
            breederId, litterId
        );

        return Task.CompletedTask;
    }
}