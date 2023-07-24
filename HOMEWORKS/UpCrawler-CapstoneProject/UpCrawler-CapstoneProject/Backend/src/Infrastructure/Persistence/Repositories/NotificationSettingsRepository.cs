using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories
{
    public class NotificationSettingsRepository : INotificationSettingsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NotificationSettingsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SaveNotificationSettings(NotificationSetting notificationSetting)
        {
            // Save the notification settings to the database using Entity Framework
            _applicationDbContext.NotificationSettings.Add(notificationSetting);
            await _applicationDbContext.SaveChangesAsync();
        }

    }
}
