using static SoccerPlus.Domain.SeedWork.Notification.NotificationModel;

namespace SoccerPlus.Domain.SeedWork.Notification
{
    public interface INotification
    {
        NotificationModel NotificationModel { get; }
        bool HasNotification { get; }
        void AddNotification(string key, string message, ENotificationType notificationType);
    }

}
