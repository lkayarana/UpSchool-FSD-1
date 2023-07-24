using Application.Common.Models.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class SeleniumLogHub : Hub
    {
        public async Task SendLogNotificationAsync(SeleniumLogDto log)
        {
            try
            {
                await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSeleniumLogAdded", log);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Hata: " + ex.Message);
                Console.WriteLine("Ayrıntılar: " + ex.StackTrace);
            }
        }

        public async Task SendProductLogNotificationAsync(SeleniumLogDto productLog)
        {
            try
            {
                await Clients.AllExcept(Context.ConnectionId).SendAsync("NewProductLogAdded", productLog);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Hata: " + ex.Message);
                Console.WriteLine("Ayrıntılar: " + ex.StackTrace);
            }
        }

    }
}
