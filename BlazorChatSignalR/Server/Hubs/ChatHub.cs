using BlazorChatSignalR.Server.Data;
using BlazorChatSignalR.Server.Models;
using Microsoft.AspNetCore.SignalR;


namespace BlazorChatSignalR.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> Users = new Dictionary<string, string>();
        private readonly DataContext _context;
        private static User utilisateur;
        private static Message msg;


       public ChatHub(DataContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            string username = Context.GetHttpContext().Request.Query["username"];
            utilisateur.Name = username;

            _context.Users.Add(utilisateur);
            await _context.SaveChangesAsync();
            Users.Add(Context.ConnectionId, username);
            await AddMessageToChat(string.Empty, $"{username} joined the party!");
            await base.OnConnectedAsync();
            


        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await AddMessageToChat(string.Empty, $"{username} left!");
        }

        public async Task AddMessageToChat(string user, string message)
        {
            var userList = await _context.Users.ToListAsync();
            foreach (var item in userList)
            {
                if (item.Name == user)
                {
                    msg.Id = item.Id;
                    msg.contenu = message;
                }
            }
            _context.Messages.Add(msg);
            await Clients.All.SendAsync("GetThatMessageDude", user, message);
            await _context.SaveChangesAsync();


        }
    }
}
