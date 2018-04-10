using System;
using System.Net.Sockets;
using Screener.Core;
using Screener.Core.Models.Messages;

namespace Screener.Client {

    public class ScreenerClient : ClientConnection {

        public Action<ProcessScreenMessage> OnProcessScreenMessage { get; set; }

        public ScreenerClient(string adress, int tcpPort, int udpReceivePort, int udpSendPort) : base(new TcpClient(), udpReceivePort, udpSendPort) {
            TcpClient.Connect(adress, tcpPort);
            Status = Status.Connected;
        }

        protected override void OnTcpMessageReceived(MessageBase message) {
            throw new NotImplementedException();
        }

        protected override void OnUdpMessageReceived(MessageBase message) {
            switch (message) {
                case ProcessScreenMessage processScreenMessage:
                    OnProcessScreenMessage?.Invoke(processScreenMessage);
                    break;
            }
        }

    }

}