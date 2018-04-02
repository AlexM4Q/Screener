using System.Net.Sockets;
using Screener.Core;
using Screener.Core.Models.Messages;

namespace Screener.Server {

    public class ScreenerConnection : ClientConnection {

        public ScreenerConnection(TcpClient client) : base(client) {
            Status = Status.Connected;
        }

        protected override void OnMessageReceived(MessageBase messageBase) {
            throw new System.NotImplementedException();
        }

    }

}