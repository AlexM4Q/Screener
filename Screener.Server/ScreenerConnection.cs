using System;
using System.Net.Sockets;
using Screener.Core.Connection;
using Screener.Core.Models.Messages;

namespace Screener.Server {

    public class ScreenerConnection : ClientConnection {

        public ScreenerConnection(TcpClient client, int udpReceivePort, int udpSendPort) : base(client, udpReceivePort, udpSendPort) {
            Status = Status.Connected;

            Start();
        }

        protected override void OnTcpMessageReceived(MessageBase message) {
            throw new NotImplementedException();
        }

        protected override void OnUdpMessageReceived(MessageBase message) {
            throw new NotImplementedException();
        }
    }

}