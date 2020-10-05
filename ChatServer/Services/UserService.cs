using ChatServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer.Services
{
    public class UserService
    {
        private List<User> Users = new List<User>();

        public WebSocket GetSocketById(string id)
        {
            return Users.FirstOrDefault(u => u.Id == id).Websocket;
        }

        public User Get(string nickname)
        {
            return Users.FirstOrDefault(u => u.Nickname == nickname);
        }
        public User Get(WebSocket socket)
        {
            return Users.FirstOrDefault(u => u.Websocket == socket);
        }

        public List<User> GetAll()
        {
            return Users;
        }


        public void AddSocket(WebSocket socket)
        {
            Users.Add(new User(socket));
        }

        public void Login(string id, string nickname)
        {
            Users.FirstOrDefault(u => u.Id == id)?.Login(nickname);            
        }

        public async Task RemoveSocket(string id)
        {
            var user = Users.First(u => u.Id == id);
            Users.Remove(user);
            await user.Websocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the ConnectionManager",
                                    cancellationToken: CancellationToken.None);
        }

    }
}
