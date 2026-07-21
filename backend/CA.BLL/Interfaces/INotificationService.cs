namespace CA.BLL.Interfaces;

public interface INotificationService
{
    Task NotifyLitterPublishedAsync(Guid breederId, Guid litterId);
}