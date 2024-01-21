using SoccerPlus.Domain.SeedWork.Exceptions;
using SoccerPlus.Domain.SeedWork.Notification;

namespace SoccerPlus.Application.Common
{
    public abstract class BaseService
    {
        protected readonly INotification _notification;


        public BaseService(INotification notification)
        {
            _notification = notification;
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (NotFoundException e)
            {
                _notification.AddNotification("Not Found", e.Message, NotificationModel.ENotificationType.NotFound);
            }
            catch (ArgumentException e)
            {
                _notification.AddNotification("Invalid Property", e.Message, NotificationModel.ENotificationType.BadRequestError);
            }
            catch (Exception e)
            {
                _notification.AddNotification("Internal Error", e.Message, NotificationModel.ENotificationType.InternalServerError);
            }
            return default;
        }
    }

}
