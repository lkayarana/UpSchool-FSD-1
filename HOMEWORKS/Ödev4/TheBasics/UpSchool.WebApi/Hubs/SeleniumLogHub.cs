using Microsoft.AspNetCore.SignalR;

namespace UpSchool.WebApi.Hubs
{
    public class SeleniumLogHub:Hub
    {
        public async Task SendLogNotificationAsync(SeleniumLogHub log)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSeleniumLogAdded",log);
        }
    }
}
