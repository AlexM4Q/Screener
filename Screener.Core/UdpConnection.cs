using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using Screener.Core.Models.Messages;

namespace Screener.Core {

    public class UdpConnection : RemoteConnection {

        private readonly int _sendPort;

        protected MessageBase UdpMessage;

        internal readonly UdpClient UdpClient;

        public UdpConnection(int receivePort, int sendPort) {
            _sendPort = sendPort;

            UdpClient = new UdpClient(receivePort);
        }

        public override void Dispose() => UdpClient?.Dispose();

        public void Send(MessageBase message) => UdpMessage = message;

        protected override void Receiving() {
            IPEndPoint remoteIp = null;

            try {
                while (Connected) {
                    var data = UdpClient.Receive(ref remoteIp);

                    using (var stream = new MemoryStream(data)) {
                        OnMessage(Serializer.DeserializeWithLengthPrefix<MessageBase>(stream, PrefixStyle.Fixed32));
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void Sending() {
            try {
                while (Connected) {
                    if (UdpMessage == null) continue;
                    lock (UdpMessage) {
                        using (var stream = new MemoryStream()) {
                            Serializer.SerializeWithLengthPrefix(stream, UdpMessage, PrefixStyle.Fixed32);

                            var data = stream.ToArray();
                            UdpClient.Send(data, data.Length, "localhost", _sendPort);
                        }
                    }

                    UdpMessage = null;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

    }

}