using ChatServer.Services;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketManager
{
    public abstract class WebSocketHandler
    {
        protected UserService Service { get; set; }

        public WebSocketHandler(UserService Service)
        {
            this.Service = Service;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            Service.AddSocket(socket);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await Service.RemoveSocket(Service.Get(socket).Id);
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if(socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.UTF8.GetBytes(message),
                                                                  offset: 0, 
                                                                  count: message.Length),
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);          
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(Service.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach(var user in Service.GetAll())
            {
                if(user.Websocket.State == WebSocketState.Open && user.Logged)
                    await SendMessageAsync(user.Websocket, message);
            }
        }

        public async Task SendMessageToOneAsync(string message, string nickname)
        {
            var user = Service.Get(nickname);
            if(user.Websocket.State == WebSocketState.Open && user.Logged)
                await SendMessageAsync(user.Websocket, message);
        }

        public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}