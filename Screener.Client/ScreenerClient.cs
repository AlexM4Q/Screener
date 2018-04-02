using System;
using System.Net.Sockets;
using Screener.Core;
using Screener.Core.Models.Messages;

namespace Screener.Client {

    public class ScreenerClient : ClientConnection {

        public Action<ProcessScreenMessage> OnProcessScreenMessage { get; set; }

        public ScreenerClient(string adress, int port) : base(new TcpClient()) {
            Client.Connect(adress, port);
            Status = Status.Connected;
        }

        protected override void OnMessageReceived(MessageBase messageBase) {
            switch (messageBase) {
                case ProcessScreenMessage processScreenMessage:
                    OnProcessScreenMessage?.Invoke(processScreenMessage);
                    break;
            }
        }

    }

}