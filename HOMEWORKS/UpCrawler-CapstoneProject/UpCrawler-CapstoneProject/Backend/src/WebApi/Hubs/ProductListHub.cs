using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class ProductListHub:Hub
    {
        public async Task UpdateProductList(List<Product> productsList)
        {
            
            await Clients.All.SendAsync("ProductListUpdated", productsList);
        }
    }
}
