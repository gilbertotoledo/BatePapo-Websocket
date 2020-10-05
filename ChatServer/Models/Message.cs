using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServe.Models
{
    public class Message
    {
        public const string Login = "/l";
        public const string MessageToRoom = "/mr";
        public const string MessagePrivate = "/mp";
        public const string EnteredRoom = "/er";
        public const string ExitedRoom = "/xr";
        public const string ReceiveMessage = "/rm";

        public string Command { get; set; }
        public string Content { get; set; }
        public string ToNickname { get; set; }

        public Message(string text)
        {
            this.Command = text.Substring(0, text.IndexOf(' '));
            this.Content = text.Substring(text.IndexOf(' ') + 1);

            if(this.Content.Contains("@"))
            {
                this.ToNickname = this.Content.Substring(0, this.Content.IndexOf(' ')).Replace("@","");
                this.Content = this.Content.Substring(this.Content.IndexOf(' ') + 1);
            }
        }
    }
}
