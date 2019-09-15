using API.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.SignalR
{
    public class MessageHub: Hub
    {
        private readonly static UserMapping<string> _connections =
                    new UserMapping<string>();

        public async Task SendMessageAsync(string me, string reciver = null, string message = null)
        {
            await Clients.All.SendAsync(me, message);
        }
        public override async Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;
            _connections.Add(name, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, "Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;
            _connections.Remove(name, Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Users");
            await base.OnDisconnectedAsync(exception);
        }

    }
}

