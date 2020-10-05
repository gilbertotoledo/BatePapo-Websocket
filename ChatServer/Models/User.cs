using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public bool Logged { get; set; } = false;
        public WebSocket Websocket { get; set; }

        public User(WebSocket websocket)
        {
            Id = Guid.NewGuid().ToString();
            Logged = false;
            Websocket = websocket;
        }

        public void Login(string nickname)
        {
            this.Nickname = nickname;
            this.Logged = true;
        }
    }
}
