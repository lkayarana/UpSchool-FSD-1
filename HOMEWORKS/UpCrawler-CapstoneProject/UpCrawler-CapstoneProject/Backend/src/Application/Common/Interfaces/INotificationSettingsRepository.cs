using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface INotificationSettingsRepository
    {
        Task SaveNotificationSettings(NotificationSetting notificationSettings);
    }
}
