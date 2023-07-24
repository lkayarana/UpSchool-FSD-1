using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class OrderHub:Hub
    {

        public async Task SendUserData(string requestedAmount, string productCrawlType)
        {
            await Clients.All.SendAsync("UserDataReceived",requestedAmount,  productCrawlType);
        }
    }
}
