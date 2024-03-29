﻿namespace SoccerPlus.Domain.SeedWork.Notification
{
    public class NotificationModel
    {
        public Guid NotificationId { get; private set; }
        public string Key { get; private set; }
        public string Message { get; private set; }
        public ENotificationType NotificationType { get; set; }

        public NotificationModel(string key, string message)
        {
            NotificationId = Guid.NewGuid();
            Key = key;
            Message = message;
            NotificationType = ENotificationType.BusinessRules;
        }

        public NotificationModel(string key, string message, ENotificationType notificationType)
        {
            NotificationId = Guid.NewGuid();
            Key = key;
            Message = message;
            NotificationType = notificationType;
        }

        public void UpdateMessage(string message, string key)
        {
            Message = message;
            Key = key;
        }

        public enum ENotificationType : byte
        {
            Default = 0,
            InternalServerError = 1,
            BusinessRules = 2,
            NotFound = 3,
            BadRequestError = 4,
        }
    }

}
