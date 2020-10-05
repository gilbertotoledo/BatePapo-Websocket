using ChatServe.Models;
using ChatServer.Services;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketManager;

namespace ChatServer
{
    public class ChatHandler : WebSocketHandler
    {
        public ChatHandler(UserService service) : base(service)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = Service.Get(socket).Id;
        }
        public override async Task OnDisconnected(WebSocket socket)
        {
            var user = Service.Get(socket);
            await SendMessageToAllAsync($"{Message.ExitedRoom} {user.Nickname} saiu da sala.");
            await base.OnDisconnected(socket);
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var user = Service.Get(socket);
            var message = new Message(Encoding.UTF8.GetString(buffer, 0, result.Count));

            switch (message.Command)
            {
                case Message.Login:
                    Service.Login(user.Id, message.Content);
                    await SendMessageToAllAsync($"{Message.EnteredRoom} {message.Content} entrou na sala #geral.");
                    break;
                case Message.MessageToRoom:
                    if(!string.IsNullOrEmpty(message.ToNickname))
                        await SendMessageToAllAsync($"{Message.ReceiveMessage} {user.Nickname} diz para {message.ToNickname}: {message.Content}");
                    else
                        await SendMessageToAllAsync($"{Message.ReceiveMessage} {user.Nickname} diz: {message.Content}");
                    break;
                case Message.MessagePrivate:
                    await SendMessageToOneAsync($"{Message.ReceiveMessage} {user.Nickname} diz para {message.ToNickname} (privado): {message.Content}", message.ToNickname);
                    await SendMessageToOneAsync($"{Message.ReceiveMessage} {user.Nickname} diz para você (privado): {message.Content}",message.ToNickname);
                    break;
            }
        }
    }
}
