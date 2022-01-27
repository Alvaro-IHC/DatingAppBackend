using System.Threading.Tasks;
using DatingApp.API.Hubs;
using DatingApp.API.Hubs.Clients;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public ChatController(IHubContext<ChatHub, IChatClient> chatHub)
        {
            _chatHub = chatHub;
        }

        [HttpPost("/messages")]
        public async Task Post(ChatMessage message)
        {
            // run some logic...
            await _chatHub.Clients.All.ReceiveMessage(message);
        }
    }
}