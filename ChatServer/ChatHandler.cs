using ChatServe.Models;
using ChatServer.Services;
using System.Diagnostics.Eventing.Reader;
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
                    if(Service.Login(user.Id, message.Content))
                    {
                        await SendMessageToAllAsync($"{Message.EnteredRoom} {message.Content} entrou na sala #geral.");
                    }
                    else
                    {
                        await SendMessageToOneAsync($"{Message.LoginError} Nome de usuário já existe.", socket);
                    }
                    break;
                case Message.MessageToRoom:
                    if (!string.IsNullOrEmpty(message.ToNickname))
                    {
                        var toUser = Service.Get(message.ToNickname);
                        if (toUser == null)
                        {
                            await SendMessageToOneAsync($"{Message.MentionError} {message.ToNickname} não encontrado na sala.", socket);
                        }
                        else
                        {
                            await SendMessageToAllAsync($"{Message.ReceiveMessage} {user.Nickname} diz para {message.ToNickname}: {message.Content}");
                        }
                    }
                    else
                    {
                        await SendMessageToAllAsync($"{Message.ReceiveMessage} {user.Nickname} diz: {message.Content}");
                    }
                    break;
                case Message.MessagePrivate:
                    await SendMessageToOneAsync($"{Message.ReceiveMessagePrivate} {user.Nickname} diz para {message.ToNickname} (privado): {message.Content}", message.ToNickname);
                    await SendMessageToOneAsync($"{Message.ReceiveMessagePrivate} {user.Nickname} diz para {message.ToNickname} (privado): {message.Content}", socket);
                    break;
            }
        }
    }
}
