using System;
using System.Net.Sockets;
using Screener.Core.Models.Messages;

namespace Screener.Core.Connection {

    public abstract class ClientConnection : IDisposable {

        protected readonly TcpConnection TcpConnection;

        protected readonly UdpConnection UdpConnection;

        protected Status Status { get; set; }

        public TcpClient TcpClient => TcpConnection.TcpClient;

        public UdpClient UdpClient => UdpConnection.UdpClient;

        protected ClientConnection(TcpClient client, int udpReceivePort, int udpSendPort) {
            TcpConnection = new TcpConnection(client) {
                OnMessage = OnTcpMessageReceived,
                GetStatus = () => Status
            };
            UdpConnection = new UdpConnection(udpReceivePort, udpSendPort) {
                OnMessage = OnUdpMessageReceived,
                GetStatus = () => Status
            };
        }

        public void Dispose() {
            Status = Status.Disconnected;
            TcpConnection.Dispose();
            UdpConnection.Dispose();
        }

        public void SendViaTcp(MessageBase message) => TcpConnection.Send(message);

        public void SendViaUdp(MessageBase message) => UdpConnection.Send(message);

        protected abstract void OnTcpMessageReceived(MessageBase message);

        protected abstract void OnUdpMessageReceived(MessageBase message);

    }

}