using System.Net.Sockets;
using Screener.Core;
using Screener.Core.Models.Messages;

namespace Screener.Server {

    public class ScreenerConnection : ClientConnection {

        public ScreenerConnection(TcpClient client, int udpReceivePort, int udpSendPort) : base(client, udpReceivePort, udpSendPort) {
            Status = Status.Connected;
        }

        protected override void OnTcpMessageReceived(MessageBase message) {
            throw new System.NotImplementedException();
        }

        protected override void OnUdpMessageReceived(MessageBase message) {
            throw new System.NotImplementedException();
        }

    }

}