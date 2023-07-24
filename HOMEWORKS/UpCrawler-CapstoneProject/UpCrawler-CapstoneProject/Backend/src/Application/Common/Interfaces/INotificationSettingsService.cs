using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface INotificationSettingsService
    {
        Task SaveNotificationSettings(NotificationSetting notificationSettings);
    }
}
