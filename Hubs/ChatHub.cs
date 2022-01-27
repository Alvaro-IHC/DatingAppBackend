using Microsoft.AspNetCore.SignalR;
using DatingApp.API.Models;
using System.Threading.Tasks;
using DatingApp.API.Hubs.Clients;

namespace DatingApp.API.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        // public async Task SendMessage(ChatMessage message)
        // {
        //     await Clients.All.ReceiveMessage(message);
        // }
    }
}