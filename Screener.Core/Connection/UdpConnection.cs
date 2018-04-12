using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using Screener.Core.Models.Messages;
using Screener.Core.Transaction;

namespace Screener.Core.Connection
{
    public class UdpConnection : RemoteConnection
    {
        private readonly int _sendPort;

        protected MessageBase UdpMessage;

        internal readonly UdpClient UdpClient;

        private readonly TransactionManager _transactionManager;

        public UdpConnection(int receivePort, int sendPort)
        {
            _sendPort = sendPort;

            UdpClient = new UdpClient(receivePort);

            _transactionManager = new TransactionManager(60000, Sending);
        }

        public override void Dispose() => UdpClient?.Dispose();

        public void Send(MessageBase message) => UdpMessage = message;

        protected override void Receiving()
        {
            IPEndPoint remoteIp = null;

            try
            {
                while (Connected)
                {
                    var data = UdpClient.Receive(ref remoteIp);

                    using (var stream = new MemoryStream(data))
                    {
                        var message = Serializer.DeserializeWithLengthPrefix<MessageBase>(stream, PrefixStyle.Fixed32);
                        if (message is TransactionMessage transaction)
                        {
                            var build = _transactionManager.Receive(transaction);
                            if (build != null)
                            {
                                OnMessage(build);
                            }
                        }
                        else
                        {
                            OnMessage(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void Sending()
        {
            try
            {
                while (Connected)
                {
                    if (UdpMessage == null) continue;
                    lock (UdpMessage)
                    {
                        _transactionManager.Send(UdpMessage);
                    }

                    UdpMessage = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void Sending(MessageBase message)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Fixed32);

                var data = stream.ToArray();
                UdpClient.Send(data, data.Length, "localhost", _sendPort);
            }
        }
    }
}