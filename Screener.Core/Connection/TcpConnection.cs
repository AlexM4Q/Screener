using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using ProtoBuf;
using Screener.Core.Models.Messages;

namespace Screener.Core.Connection {

    public class TcpConnection : RemoteConnection {

        protected readonly IList<MessageBase> MessagesQueue;

        internal readonly TcpClient TcpClient;

        public TcpConnection(TcpClient client) {
            MessagesQueue = new List<MessageBase>();

            TcpClient = client;
            TcpClient.ReceiveBufferSize = 16 * 1024;
            TcpClient.SendBufferSize = 16 * 1024;
        }

        public override void Dispose() => TcpClient?.Dispose();

        public void Send(MessageBase message) => MessagesQueue.Add(message);

        protected override void Receiving() {
            while (Connected) {
                if (TcpClient.Available > 0) {
                    try {
                        OnMessage(Serializer.DeserializeWithLengthPrefix<MessageBase>(TcpClient.GetStream(), PrefixStyle.Fixed32));
                    } catch (Exception e) {
                        Debug.WriteLine(e);
                    }
                }

                Thread.Sleep(30);
            }
        }

        protected override void Sending() {
            while (Connected) {
                if (MessagesQueue.Count > 0) {
                    var message = MessagesQueue[0];

                    try {
                        Serializer.SerializeWithLengthPrefix(TcpClient.GetStream(), message, PrefixStyle.Fixed32);
                    } catch (Exception e) {
                        Debug.WriteLine(e);
                    } finally {
                        MessagesQueue.Remove(message);
                    }
                }

                Thread.Sleep(30);
            }
        }

    }

}