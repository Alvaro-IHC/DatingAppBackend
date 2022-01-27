using DatingApp.API.Models;
using System.Threading.Tasks;

namespace DatingApp.API.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
        
    }
}