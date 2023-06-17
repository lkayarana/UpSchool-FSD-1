using ChatBot.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatBot.Hubs
{
    public class ChatHub : Hub
    {
        private static List<User> users = new List<User>();
        private static List<Message> messages = new List<Message>();


        public List<User> GetAllUsers()
        {
            return users;
        }

        public List<Message> GetAllMessages()
        {
            return messages;
        }

        public async Task LogIn(string username)
        {
            var userName = new User()
            {
                UserName = username,
            };

            users.Add(userName);

            await Clients.AllExcept(Context.ConnectionId).SendAsync("UserLoggedIn", userName);
        }

        public async Task SendMessage(string userName, string text)
        {
            var message = new Message()
            {
                UserName = userName,
                Text = text,
                CreatedOn = DateTime.Now
            };

            messages.Add(message);

            await Clients.AllExcept(Context.ConnectionId).SendAsync("MessageReceived", message);
        }

        public async Task UserLeftTheChat(User userName)
        {
            users.Remove(userName);

            await Clients.AllExcept(Context.ConnectionId).SendAsync("UserLeft", userName);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ConnectedUsers", users);
            await base.OnConnectedAsync();
        }
    }
}
